using System;
using System.Collections.Generic;
using System.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;
using Dnn.Flow.QuizLearn.Models;


namespace Dnn.Flow.QuizLearn.Data
{
    public class SqlDataProvider : IQuizLearnDataRepository
    {
        private const string ProviderType = "data";

        private readonly string _connectionString;
        private readonly string _databaseOwner;
        private readonly string _objectQualifier;

        public SqlDataProvider()
        {
            var providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
            var objprovider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

            _connectionString = Config.GetConnectionString();
            _databaseOwner = objprovider.Attributes["databaseOwner"];
            _objectQualifier = objprovider.Attributes["objectQualifier"];

        }

        private string GetFullyQualifiedName(string name)
        {
            return _databaseOwner + _objectQualifier + "lm_" + name;
            //return {_databaseOwner}+ {_objectQualifier} + "lm_" + {name};
        }


        //---------------------
        // Lekérdezések
        //---------------------

        public IEnumerable<LanguageInfo> GetAllActiveLanguages()
        {
            var languages = new List<LanguageInfo>();
            using (IDataReader reader = SqlHelper.ExecuteReader(
                _connectionString,
                CommandType.StoredProcedure,
                GetFullyQualifiedName("Languages_GetAllActive")))
            {
                while (reader.Read())
                {
                    languages.Add(new LanguageInfo
                    {
                        LanguageId = Null.SetNullInteger(reader["LanguageId"]),
                        Name = Null.SetNullString(reader["Name"]),
                        Code = Null.SetNullString(reader["Code"]),
                        IsActive = Null.SetNullBoolean(reader["IsActive"])
                    });
                }
            }
            return languages;
        }

        public IEnumerable<QuestionLevelInfo> GetAllQuestionLevels()
        {
            var levels = new List<QuestionLevelInfo>();

            using (IDataReader reader = SqlHelper.ExecuteReader(
                _connectionString,
                CommandType.StoredProcedure,
                GetFullyQualifiedName("QuestionLevels_GetAll")))
            {
                while (reader.Read())
                {
                    levels.Add(new QuestionLevelInfo
                    {
                        QuestionLevelId = Null.SetNullInteger(reader["QuestionLevelId"]),
                        LevelName = Null.SetNullString(reader["LevelName"]),
                        LevelOrder = Null.SetNullInteger(reader["LevelOrder"])
                    });
                }
            }

            return levels;
        }

        public IEnumerable<SkillTypeInfo> GetAllSkillTypes()
        {
            var skillTypes = new List<SkillTypeInfo>();
            using (IDataReader reader = SqlHelper.ExecuteReader(
                _connectionString,
                CommandType.StoredProcedure,
                GetFullyQualifiedName("SkillTypes_GetAll")))
            {
                while (reader.Read())
                {
                    skillTypes.Add(new SkillTypeInfo
                    {
                        SkillTypeId = Null.SetNullInteger(reader["SkillTypeId"]),
                        SkillName = Null.SetNullString(reader["SkillName"])
                    });
                }
            }
            return skillTypes;
        }

        public IEnumerable<PaceTypeInfo> GetAllPaceTypes()
        {
            var paceTypes = new List<PaceTypeInfo>();
            using (IDataReader reader = SqlHelper.ExecuteReader(
                _connectionString,
                CommandType.StoredProcedure,
                GetFullyQualifiedName("PaceTypes_GetAll")))
            {
                while (reader.Read())
                {
                    paceTypes.Add(new PaceTypeInfo
                    {
                        PaceTypeId = Null.SetNullInteger(reader["PaceTypeId"]),
                        PaceKey = Null.SetNullString(reader["PaceKey"]),
                        PaceName = Null.SetNullString(reader["PaceName"]),
                        MinProducts = Null.SetNullInteger(reader["MinProducts"]),
                        MaxProducts = Null.SetNullInteger(reader["MaxProducts"]),
                        IsActive = Null.SetNullBoolean(reader["IsActive"])
                    });
                }
            }
            return paceTypes;
        }


        public AssessmentModeInfo GetAssessmentModeByKey(string modeKey)
        {
            AssessmentModeInfo mode = null;

            using (IDataReader reader = SqlHelper.ExecuteReader(
                _connectionString,
                CommandType.StoredProcedure,
                GetFullyQualifiedName("AssessmentModes_GetByKey"),
                new System.Data.SqlClient.SqlParameter("@ModeKey", modeKey)))
            {
                if (reader.Read())
                {
                    mode = new AssessmentModeInfo
                    {
                        AssessmentModeId = Null.SetNullInteger(reader["AssessmentModeId"]),
                        ModeKey = Null.SetNullString(reader["ModeKey"]),
                        ModeName = Null.SetNullString(reader["ModeName"]),
                        IsActive = Null.SetNullBoolean(reader["IsActive"])
                    };
                }
            }

            return mode;
        }

        //---------------------
        // Adatmódosítás
        //---------------------


        public int AddAssessmentSession(AssessmentSessionInfo sessionInfo)
        {
            object result = SqlHelper.ExecuteScalar(
                _connectionString,
                CommandType.StoredProcedure,
                GetFullyQualifiedName("AssessmentSessions_Add"),
                new System.Data.SqlClient.SqlParameter("@ModuleId", sessionInfo.ModuleId),
                new System.Data.SqlClient.SqlParameter("@AssessmentModeId", sessionInfo.AssessmentModeId),
                new System.Data.SqlClient.SqlParameter("@LanguageId", sessionInfo.LanguageId),
                new System.Data.SqlClient.SqlParameter("@SecondaryLanguageId", (object)sessionInfo.SecondaryLanguageId ?? DBNull.Value),
                new System.Data.SqlClient.SqlParameter("@SelectedLevelId", (object)sessionInfo.SelectedLevelId ?? DBNull.Value),
                new System.Data.SqlClient.SqlParameter("@PaceTypeId", (object)sessionInfo.PaceTypeId ?? DBNull.Value),
                new System.Data.SqlClient.SqlParameter("@UserId", (object)sessionInfo.UserId ?? DBNull.Value),
                new System.Data.SqlClient.SqlParameter("@NeedLevelTest", sessionInfo.NeedLevelTest),
                new System.Data.SqlClient.SqlParameter("@Status", sessionInfo.Status));

            return Convert.ToInt32(result);
        }


        public int AddAssessmentSessionSkill(int moduleId, int assessmentSessionId, int skillTypeId)
        {
            object result = SqlHelper.ExecuteScalar(
                _connectionString,
                CommandType.StoredProcedure,
                GetFullyQualifiedName("AssessmentSessionSkills_Add"),
                new System.Data.SqlClient.SqlParameter("@ModuleId", moduleId),
                new System.Data.SqlClient.SqlParameter("@AssessmentSessionId", assessmentSessionId),
                new System.Data.SqlClient.SqlParameter("@SkillTypeId", skillTypeId));

            return Convert.ToInt32(result);
        }

        public int CompleteAssessmentSession(int moduleId, int assessmentSessionId, int? finalLevelId)
        {
            object result = SqlHelper.ExecuteScalar(
                _connectionString,
                CommandType.StoredProcedure,
                GetFullyQualifiedName("AssessmentSessions_Complete"),
                new System.Data.SqlClient.SqlParameter("@ModuleId", moduleId),
                new System.Data.SqlClient.SqlParameter("@AssessmentSessionId", assessmentSessionId),
                new System.Data.SqlClient.SqlParameter("@FinalLevelId", (object)finalLevelId ?? DBNull.Value));

            return Convert.ToInt32(result);
        }


        //---------------------
        // Eredmények
        //---------------------

        public int AddRecommendationResult(RecommendationResultInfo resultInfo)
        {
            object result = SqlHelper.ExecuteScalar(
                _connectionString,
                CommandType.StoredProcedure,
                GetFullyQualifiedName("RecommendationResults_Add"),
                new System.Data.SqlClient.SqlParameter("@ModuleId", resultInfo.ModuleId),
                new System.Data.SqlClient.SqlParameter("@AssessmentSessionId", resultInfo.AssessmentSessionId),
                new System.Data.SqlClient.SqlParameter("@SourceType", resultInfo.SourceType),
                new System.Data.SqlClient.SqlParameter("@RecommendedLevelId", (object)resultInfo.RecommendedLevelId ?? DBNull.Value),
                new System.Data.SqlClient.SqlParameter("@RecommendationSummary", (object)resultInfo.RecommendationSummary ?? DBNull.Value),
                new System.Data.SqlClient.SqlParameter("@MinProducts", resultInfo.MinProducts),
                new System.Data.SqlClient.SqlParameter("@MaxProducts", resultInfo.MaxProducts));

            return Convert.ToInt32(result);
        }


        public int AddRecommendationResultItem(RecommendationResultItemInfo itemInfo)
        {
            object result = SqlHelper.ExecuteScalar(
                _connectionString,
                CommandType.StoredProcedure,
                GetFullyQualifiedName("RecommendationResultItems_Add"),
                new System.Data.SqlClient.SqlParameter("@ModuleId", itemInfo.ModuleId),
                new System.Data.SqlClient.SqlParameter("@RecommendationResultId", itemInfo.RecommendationResultId),
                new System.Data.SqlClient.SqlParameter("@ProductSKU", itemInfo.ProductSKU),
                new System.Data.SqlClient.SqlParameter("@SortOrder", itemInfo.SortOrder),
                new System.Data.SqlClient.SqlParameter("@SourceRule", (object)itemInfo.SourceRule ?? DBNull.Value),
                new System.Data.SqlClient.SqlParameter("@SourceType", itemInfo.SourceType),
                new System.Data.SqlClient.SqlParameter("@Score", (object)itemInfo.Score ?? DBNull.Value),
                new System.Data.SqlClient.SqlParameter("@IsPrimary", itemInfo.IsPrimary));

            return Convert.ToInt32(result);
        }


        //---------------------
        // Biztonság
        //---------------------


        //public IEnumerable<RecommendationRuleInfo> FindExactRecommendationRules(
        //    int moduleId,
        //    int languageId,
        //    int questionLevelId,
        //    int skillTypeId,
        //    int paceTypeId,
        //    int? secondaryLanguageId)
        //{
        //    var rules = new List<RecommendationRuleInfo>();

        //    using (IDataReader reader = SqlHelper.ExecuteReader(
        //        _connectionString,
        //        CommandType.StoredProcedure,
        //        GetFullyQualifiedName("RecommendationRules_FindExact"),
        //        new System.Data.SqlClient.SqlParameter("@ModuleId", moduleId),
        //        new System.Data.SqlClient.SqlParameter("@LanguageId", languageId),
        //        new System.Data.SqlClient.SqlParameter("@QuestionLevelId", questionLevelId),
        //        new System.Data.SqlClient.SqlParameter("@SkillTypeId", skillTypeId),
        //        new System.Data.SqlClient.SqlParameter("@PaceTypeId", paceTypeId),
        //        new System.Data.SqlClient.SqlParameter("@SecondaryLanguageId", (object)secondaryLanguageId ?? DBNull.Value))) ;
        //    {
        //        while (reader.Read())
        //        {
        //            rules.Add(new RecommendationRuleInfo
        //            {
        //                RecommendationRuleId = Null.SetNullInteger(reader["RecommendationRuleId"]),
        //                ModuleId = Null.SetNullInteger(reader["ModuleId"]),
        //                LanguageId = Null.SetNullInteger(reader["LanguageId"]),
        //                QuestionLevelId = Null.SetNullInteger(reader["QuestionLevelId"]),
        //                SkillTypeId = Null.SetNullInteger(reader["SkillTypeId"]),
        //                SecondaryLanguageId = Null.SetNullInteger(reader["SecondaryLanguageId"]),
        //                PaceTypeId = Null.SetNullInteger(reader["PaceTypeId"]),
        //                Priority = Null.SetNullInteger(reader["Priority"]),
        //                MatchMode = Null.SetNullString(reader["MatchMode"]),
        //                HotcakesProductSKU = Null.SetNullString(reader["HotcakesProductSKU"]),
        //                HotcakesCategoryKey = Null.SetNullString(reader["HotcakesCategoryKey"]),
        //                HotcakesTag = Null.SetNullString(reader["HotcakesTag"]),
        //                MinProducts = Null.SetNullInteger(reader["MinProducts"]),
        //                MaxProducts = Null.SetNullInteger(reader["MaxProducts"])
        //            });
        //        }
        //    }

        //    return rules;
        //}

        //public IEnumerable<RecommendationRuleInfo> FindFallbackRecommendationRules(
        //    int moduleId,
        //    int languageId,
        //    int questionLevelId,
        //    int skillTypeId,
        //    int? secondaryLanguageId)
        //{
        //    var rules = new List<RecommendationRuleInfo>();
        //    using (IDataReader reader = SqlHelper.ExecuteReader(
        //        _connectionString,
        //        CommandType.StoredProcedure,
        //        GetFullyQualifiedName("RecommendationRules_FindFallback"),
        //        new System.Data.SqlClient.SqlParameter("@ModuleId", moduleId),
        //        new System.Data.SqlClient.SqlParameter("@LanguageId", languageId),
        //        new System.Data.SqlClient.SqlParameter("@QuestionLevelId", questionLevelId),
        //        new System.Data.SqlClient.SqlParameter("@SkillTypeId", skillTypeId),
        //        new System.Data.SqlClient.SqlParameter("@SecondaryLanguageId", (object)secondaryLanguageId ?? DBNull.Value))) ;
        //    {
        //        while (reader.Read())
        //        {
        //            rules.Add(new RecommendationRuleInfo
        //            {
        //                RecommendationRuleId = Null.SetNullInteger(reader["RecommendationRuleId"]),
        //                ModuleId = Null.SetNullInteger(reader["ModuleId"]),
        //                LanguageId = Null.SetNullInteger(reader["LanguageId"]),
        //                QuestionLevelId = Null.SetNullInteger(reader["QuestionLevelId"]),
        //                SkillTypeId = Null.SetNullInteger(reader["SkillTypeId"]),
        //                SecondaryLanguageId = Null.SetNullInteger(reader["SecondaryLanguageId"]),
        //                PaceTypeId = Null.SetNullInteger(reader["PaceTypeId"]),
        //                Priority = Null.SetNullInteger(reader["Priority"]),
        //                MatchMode = Null.SetNullString(reader["MatchMode"]),
        //                HotcakesProductSKU = Null.SetNullString(reader["HotcakesProductSKU"]),
        //                HotcakesCategoryKey = Null.SetNullString(reader["HotcakesCategoryKey"]),
        //                HotcakesTag = Null.SetNullString(reader["HotcakesTag"]),
        //                MinProducts = Null.SetNullInteger(reader["MinProducts"]),
        //                MaxProducts = Null.SetNullInteger(reader["MaxProducts"])
        //            });
        //        }
        //    }
        //    return rules;

        //}


        //public IEnumerable<RecommendationRuleInfo> FindGeneralRecommendationRules(
        //    int moduleId,
        //    int languageId)
        //{
        //    var rules = new List<RecommendationRuleInfo>();
        //    using (IDataReader reader = SqlHelper.ExecuteReader(
        //        _connectionString,
        //        CommandType.StoredProcedure,
        //        GetFullyQualifiedName("RecommendationRules_FindGeneral"),
        //        new System.Data.SqlClient.SqlParameter("@ModuleId", moduleId),
        //        new System.Data.SqlClient.SqlParameter("@LanguageId", languageId)))
        //    {
        //        while (reader.Read())
        //        {
        //            rules.Add(new RecommendationRuleInfo
        //            {
        //                RecommendationRuleId = Null.SetNullInteger(reader["RecommendationRuleId"]),
        //                ModuleId = Null.SetNullInteger(reader["ModuleId"]),
        //                LanguageId = Null.SetNullInteger(reader["LanguageId"]),
        //                QuestionLevelId = Null.SetNullInteger(reader["QuestionLevelId"]),
        //                SkillTypeId = Null.SetNullInteger(reader["SkillTypeId"]),
        //                SecondaryLanguageId = Null.SetNullInteger(reader["SecondaryLanguageId"]),
        //                PaceTypeId = Null.SetNullInteger(reader["PaceTypeId"]),
        //                Priority = Null.SetNullInteger(reader["Priority"]),
        //                MatchMode = Null.SetNullString(reader["MatchMode"]),
        //                HotcakesProductSKU = Null.SetNullString(reader["HotcakesProductSKU"]),
        //                HotcakesCategoryKey = Null.SetNullString(reader["HotcakesCategoryKey"]),
        //                HotcakesTag = Null.SetNullString(reader["HotcakesTag"]),
        //                MinProducts = Null.SetNullInteger(reader["MinProducts"]),
        //                MaxProducts = Null.SetNullInteger(reader["MaxProducts"])
        //            });
        //        }
        //    }
        //    return rules;
        }
    }