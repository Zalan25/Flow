using Dnn.Flow.QuizLearn.Models;
using Dnn.Flow.QuizLearn.Services;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Dnn.Flow.QuizLearn.Controllers
{
    [DnnHandleError]
    public class ItemController : DnnController
    {
        private readonly LookupService _lookupService;
        private readonly AssessmentService _assessmentService;
        private readonly RecommendationService _recommendationService;

        public ItemController()
        {
            _lookupService = new LookupService();
            _assessmentService = new AssessmentService();
            _recommendationService = new RecommendationService();
        }

        public ActionResult Index()
        {
            var mode = GetModuleMode();
            if (Request.HttpMethod == "POST")
            {
                var assessmentAction = Request.Form["AssessmentAction"];

                if (assessmentAction == "StartLevelTest")
                {
                    return HandleStartLevelTestPost();
                }

                if (assessmentAction == "AnswerQuestion")
                {
                    return HandleAnswerQuestionPost();
                }

                return HandleStartPost(mode);
            }


            switch (mode)
            {
                case QuizLearnMode.LevelAssessment:
                    return View("StartAssessment", BuildStartViewModel(mode));

                case QuizLearnMode.Recommendation:
                case QuizLearnMode.RecommendationWithLevelAssessment:
                default:
                    return View("Start", BuildStartViewModel(mode));
            }
        }

        public ActionResult Start()
        {
            var mode = GetModuleMode();

            return View("Start", BuildStartViewModel(mode));
        }

        public ActionResult StartAssessment()
        {
            ViewBag.ModuleMode = GetModuleMode();

            return View("StartAssessment");
        }

        private ActionResult HandleStartPost(QuizLearnMode moduleMode)
        {
            var model = new AssessmentStartViewModel
            {
                ModuleId = ModuleContext.ModuleId,

                AssessmentModeId = int.TryParse(Request.Form["AssessmentModeId"], out int modeId)
                    ? modeId
                    : 0,

                LanguageId = int.TryParse(Request.Form["LanguageId"], out int langId)
                    ? langId
                    : 0,

                SecondaryLanguageId = int.TryParse(Request.Form["SecondaryLanguageId"], out int secLangId)
                    ? (int?)secLangId
                    : null,

                SelectedLevelId = int.TryParse(Request.Form["SelectedLevelId"], out int levelId)
                    ? (int?)levelId
                    : null,

                PaceTypeId = int.TryParse(Request.Form["PaceTypeId"], out int paceId)
                    ? (int?)paceId
                    : null,

                SelectedSkillTypeIds = Request.Form.GetValues("SelectedSkillTypeIds")
                    ?.Select(x => int.TryParse(x, out int sid) ? sid : 0)
                    .Where(x => x > 0)
                    .Distinct()
                    .ToList() ?? new List<int>(),

                ModuleMode = moduleMode
            };

            if (model.LanguageId <= 0)
            {
                var fresh = BuildStartViewModel(moduleMode);
                ViewBag.ServerValidationStep = 1;
                return View("Start", fresh);
            }

            if (!model.SelectedSkillTypeIds.Any())
            {
                var fresh = BuildStartViewModel(moduleMode);
                ViewBag.ServerValidationStep = 2;
                return View("Start", fresh);
            }

            if (!model.PaceTypeId.HasValue)
            {
                var fresh = BuildStartViewModel(moduleMode);
                ViewBag.ServerValidationStep = 4;
                return View("Start", fresh);
            }

            var needLevelTestFromForm =
                string.Equals(Request.Form["NeedLevelTest"], "true", StringComparison.OrdinalIgnoreCase);

            var needLevelTest =
                moduleMode == QuizLearnMode.LevelAssessment ||
                needLevelTestFromForm ||
                (
                    moduleMode == QuizLearnMode.RecommendationWithLevelAssessment &&
                    !model.SelectedLevelId.HasValue
                );

            if (moduleMode == QuizLearnMode.Recommendation && !model.SelectedLevelId.HasValue && !needLevelTest)
            {
                var fresh = BuildStartViewModel(moduleMode);
                ViewBag.ServerValidationStep = 3;
                return View("Start", fresh);
            }


            var sessionInfo = new AssessmentSessionInfo
            {
                ModuleId = ModuleContext.ModuleId,
                AssessmentModeId = model.AssessmentModeId,
                LanguageId = model.LanguageId,
                SecondaryLanguageId = model.SecondaryLanguageId,
                SelectedLevelId = model.SelectedLevelId ?? 1,
                PaceTypeId = model.PaceTypeId ?? 1,
                UserId = null,
                NeedLevelTest = needLevelTest,
                Status = "Started"
            };

            var sessionId = _assessmentService.StartAssessmentSession(
                sessionInfo,
                model.SelectedSkillTypeIds
            );

            if (needLevelTest)
            {
                if (sessionId <= 0)
                {
                    var fresh = BuildStartViewModel(moduleMode);
                    ViewBag.ServerValidationStep = 3;
                    return View("Start", fresh);
                }

                _assessmentService.GenerateSessionQuestions(
                    ModuleContext.ModuleId,
                    sessionId,
                    model.LanguageId
                );

                _assessmentService.StartTestAttempt(
                    ModuleContext.ModuleId,
                    sessionId,
                    1
                );

                var firstQuestion = _assessmentService.GetQuestionForAssessment(sessionId, 1);

                if (firstQuestion == null)
                {
                    return View("StartAssessment", BuildStartViewModel(moduleMode));
                }

                return View("Question", firstQuestion);
            }

            var rules = GetRecommendationRulesForAllSelectedSkills(model);

            ViewBag.SessionId = sessionId;
            ViewBag.RuleCount = rules.Count;

            return View("Recommendation", rules);
        }

        private List<RecommendationRuleInfo> GetRecommendationRulesForAllSelectedSkills(AssessmentStartViewModel model)
        {
            var result = new List<RecommendationRuleInfo>();

            foreach (var skillTypeId in model.SelectedSkillTypeIds)
            {
                var rulesForSkill = _recommendationService.GetMatchingRules(
                    ModuleContext.ModuleId,
                    model.LanguageId,
                    model.SelectedLevelId ?? 1,
                    skillTypeId,
                    model.PaceTypeId ?? 1,
                    model.SecondaryLanguageId
                ) as List<RecommendationRuleInfo>;

                if (rulesForSkill != null)
                {
                    result.AddRange(rulesForSkill);
                }
            }

            return result
                .GroupBy(x => x.RecommendationRuleId)
                .Select(g => g.First())
                .ToList();
        }

        private AssessmentStartViewModel BuildStartViewModel(QuizLearnMode mode)
        {
            return new AssessmentStartViewModel
            {
                ModuleId = ModuleContext.ModuleId,
                Languages = _lookupService.GetLanguages(),
                Levels = _lookupService.GetLevels(),
                Skills = _lookupService.GetSkills(),
                PaceTypes = _lookupService.GetPaceTypes(),
                SelectedSkillTypeIds = new List<int>(),
                ModuleMode = mode
            };
        }

        private QuizLearnMode GetModuleMode()
        {
            var modeValue = ModuleContext.Configuration.ModuleSettings["QuizLearnMode"] as string;

            int parsedMode;

            if (!int.TryParse(modeValue, out parsedMode))
            {
                return QuizLearnMode.RecommendationWithLevelAssessment;
            }

            if (!Enum.IsDefined(typeof(QuizLearnMode), parsedMode))
            {
                return QuizLearnMode.RecommendationWithLevelAssessment;
            }

            return (QuizLearnMode)parsedMode;
        }

        private ActionResult HandleStartLevelTestPost()
        {
            int sessionId = 0;

            if (int.TryParse(Request.Form["SessionId"], out sessionId) && sessionId > 0)
            {
                var existingSession = _assessmentService.GetAssessmentSessionById(sessionId);

                if (existingSession == null)
                {
                    sessionId = 0;
                }
                else
                {
                    _assessmentService.StartTestAttempt(
                        ModuleContext.ModuleId,
                        sessionId,
                        1
                    );

                    return Redirect(Url.Action("Question", "Item")
                        + "?sessionId=" + sessionId
                        + "&questionNumber=1");
                }
            }

            int languageId = 0;

            if (!int.TryParse(Request.Form["LanguageId"], out languageId) || languageId <= 0)
            {
                var fresh = BuildStartViewModel(GetModuleMode());
                ModelState.AddModelError("", "A nyelv kiválasztása kötelező.");
                return View("StartAssessment", fresh);
            }

            var sessionInfo = new AssessmentSessionInfo
            {
                ModuleId = ModuleContext.ModuleId,
                AssessmentModeId = 2,
                LanguageId = languageId,
                SecondaryLanguageId = null,
                SelectedLevelId = null,
                PaceTypeId = 1,
                UserId = null,
                NeedLevelTest = true,
                Status = "Started"
            };

            sessionId = _assessmentService.StartAssessmentSession(
                sessionInfo,
                new List<int>()
            );
            _assessmentService.GenerateSessionQuestions(
                ModuleContext.ModuleId,
                sessionId,
                languageId
                );

            if (sessionId <= 0)
            {
                var fresh = BuildStartViewModel(GetModuleMode());
                ModelState.AddModelError("", "A szintfelmérő session létrehozása sikertelen.");
                return View("StartAssessment", fresh);
            }


            _assessmentService.StartTestAttempt(
                ModuleContext.ModuleId,
                sessionId,
                1
            );

            return Redirect(Url.Action("Question", "Item")
                + "?sessionId=" + sessionId
                + "&questionNumber=1");
        }

        
        
            public ActionResult Question(int? sessionId, int? questionNumber)
        {
            if (Request.HttpMethod == "POST")
            {
                var assessmentAction = Request.Form["AssessmentAction"];

                if (assessmentAction == "AnswerQuestion")
                {
                    return HandleAnswerQuestionPost();
                }
            }

            if (!sessionId.HasValue || sessionId.Value <= 0)
            {
                return View("StartAssessment", BuildStartViewModel(GetModuleMode()));
            }

            var currentQuestionNumber = questionNumber.HasValue && questionNumber.Value > 0
                ? questionNumber.Value
                : 1;

            var model = _assessmentService.GetQuestionForAssessment(
                sessionId.Value,
                currentQuestionNumber
            );

            if (model == null)
            {
                return Redirect(Url.Action("AssessmentResult", "Item") + "?sessionId=" + sessionId.Value);
            }

            return View("Question", model);
        }


        private ActionResult HandleAnswerQuestionPost()
        {
            int sessionId = 0;
            int questionId = 0;
            int questionNumber = 1;
            int questionTypeId = 1;

            int.TryParse(Request.Form["SessionId"], out sessionId);
            int.TryParse(Request.Form["QuestionId"], out questionId);
            int.TryParse(Request.Form["QuestionNumber"], out questionNumber);
            int.TryParse(Request.Form["QuestionTypeId"], out questionTypeId);



            if (sessionId <= 0 || questionId <= 0)
            {
                return View("StartAssessment", BuildStartViewModel(GetModuleMode()));
            }

            if (questionTypeId == (int)QuestionType.SingleChoice ||
                questionTypeId == (int)QuestionType.TrueFalse)
            {
                int answerId = 0;

                if (!int.TryParse(Request.Form["AnswerId"], out answerId) || answerId <= 0)
                {
                    var currentModel = _assessmentService.GetQuestionForAssessment(sessionId, questionNumber);
                    ModelState.AddModelError("", "Kérlek, válassz egy választ.");
                    return View("Question", currentModel);
                }

                _assessmentService.SaveSingleChoiceAnswer(
                    ModuleContext.ModuleId,
                    sessionId,
                    questionId,
                    answerId
                );
            }
            else if (questionTypeId == (int)QuestionType.MultipleChoice)
            {
                var selectedAnswers = Request.Form.GetValues("SelectedAnswerIds");

                if (selectedAnswers == null || selectedAnswers.Length == 0)
                {
                    var currentModel = _assessmentService.GetQuestionForAssessment(sessionId, questionNumber);
                    ModelState.AddModelError("", "Válassz legalább egy opciót.");
                    return View("Question", currentModel);
                }

                var selectedAnswerIds = selectedAnswers
                    .Select(x => int.TryParse(x, out int id) ? id : 0)
                    .Where(x => x > 0)
                    .Distinct()
                    .ToList();

                if (!selectedAnswerIds.Any())
                {
                    var currentModel = _assessmentService.GetQuestionForAssessment(sessionId, questionNumber);
                    ModelState.AddModelError("", "Érvénytelen válaszopció.");
                    return View("Question", currentModel);
                }

                _assessmentService.SaveMultipleChoiceAnswer(
                    ModuleContext.ModuleId,
                    sessionId,
                    questionId,
                    selectedAnswerIds
                );
            }
            else if (questionTypeId == (int)QuestionType.Essay ||
                     questionTypeId == (int)QuestionType.Translation)
            {
                var textAnswer = Request.Form["TextAnswer"];

                if (string.IsNullOrWhiteSpace(textAnswer))
                {
                    var currentModel = _assessmentService.GetQuestionForAssessment(sessionId, questionNumber);
                    ModelState.AddModelError("", "Írj be választ.");
                    return View("Question", currentModel);
                }

                _assessmentService.SaveTextAnswer(
                    ModuleContext.ModuleId,
                    sessionId,
                    questionId,
                    textAnswer.Trim()
                );
            }
            else
            {
                var currentModel = _assessmentService.GetQuestionForAssessment(sessionId, questionNumber);
                ModelState.AddModelError("", "Ismeretlen kérdéstípus.");
                return View("Question", currentModel);
            }

            var nextQuestionNumber = questionNumber + 1;

            var nextModel = _assessmentService.GetQuestionForAssessment(
                sessionId,
                nextQuestionNumber
            );

            if (nextModel == null)
            {
                return Redirect(Url.Action("AssessmentResult", "Item") + "?sessionId=" + sessionId);
            }

            return View("Question", nextModel);
        }

        // eredmények
        public ActionResult AssessmentResult(int sessionId)
        {
            var model = _assessmentService.CalculateResult(ModuleContext.ModuleId, sessionId);

            return View("AssessmentResult", model);
        }
    }
}