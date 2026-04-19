
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* Törzsadatok */

CREATE TABLE dbo.lm__languages
(
    LanguageId INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Code NVARCHAR(10) NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_lm__languages_IsActive DEFAULT 1,

    CONSTRAINT PK_lm__languages PRIMARY KEY (LanguageId),
    CONSTRAINT UQ_lm__languages_Name UNIQUE (Name),
    CONSTRAINT UQ_lm__languages_Code UNIQUE (Code)
);

CREATE TABLE dbo.lm__question_levels
(
    QuestionLevelId INT IDENTITY(1,1) NOT NULL,
    LevelName NVARCHAR(20) NOT NULL,
    LevelOrder INT NOT NULL,

    CONSTRAINT PK_lm__question_levels PRIMARY KEY (QuestionLevelId),
    CONSTRAINT UQ_lm__question_levels_Name UNIQUE (LevelName),
    CONSTRAINT UQ_lm__question_levels_Order UNIQUE (LevelOrder)
);

CREATE TABLE dbo.lm__question_types
(
    QuestionTypeId INT IDENTITY(1,1) NOT NULL,
    TypeName NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_lm__question_types PRIMARY KEY (QuestionTypeId),
    CONSTRAINT UQ_lm__question_types UNIQUE (TypeName)
);

CREATE TABLE dbo.lm__skill_types
(
    SkillTypeId INT IDENTITY(1,1) NOT NULL,
    SkillName NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_lm__skill_types PRIMARY KEY (SkillTypeId),
    CONSTRAINT UQ_lm__skill_types UNIQUE (SkillName)
);

CREATE TABLE dbo.lm__assessment_modes
(
    AssessmentModeId INT IDENTITY(1,1) NOT NULL,
    ModeKey NVARCHAR(50) NOT NULL,
    ModeName NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_lm__assessment_modes_IsActive DEFAULT 1,

    CONSTRAINT PK_lm__assessment_modes PRIMARY KEY (AssessmentModeId),
    CONSTRAINT UQ_lm__assessment_modes_Key UNIQUE (ModeKey)
);

CREATE TABLE dbo.lm__pace_types
(
    PaceTypeId INT IDENTITY(1,1) NOT NULL,
    PaceKey NVARCHAR(50) NOT NULL,
    PaceName NVARCHAR(100) NOT NULL,
    MinProducts INT NOT NULL,
    MaxProducts INT NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_lm__pace_types_IsActive DEFAULT 1,

    CONSTRAINT PK_lm__pace_types PRIMARY KEY (PaceTypeId),
    CONSTRAINT UQ_lm__pace_types_Key UNIQUE (PaceKey),

    CONSTRAINT CK_lm__pace_types_ProductRange
    CHECK (MinProducts >= 0 AND MaxProducts >= 0 AND MinProducts <= MaxProducts)
);

/* Kérdésbank */

CREATE TABLE dbo.lm__questions
(
    QuestionId INT IDENTITY(1,1) NOT NULL,
    QuestionText NVARCHAR(1000) NOT NULL,
    LanguageId INT NOT NULL,
    QuestionLevelId INT NOT NULL,
    QuestionTypeId INT NOT NULL,
    SkillTypeId INT NOT NULL,
    Points INT NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_lm__questions_IsActive DEFAULT 1,

    CONSTRAINT PK_lm__questions PRIMARY KEY (QuestionId),

    CONSTRAINT FK_lm__questions_LanguageId FOREIGN KEY (LanguageId) REFERENCES dbo.lm__languages(LanguageId),
    CONSTRAINT FK_lm__questions_QuestionLevelId FOREIGN KEY (QuestionLevelId) REFERENCES dbo.lm__question_levels(QuestionLevelId),
    CONSTRAINT FK_lm__questions_QuestionTypeId FOREIGN KEY (QuestionTypeId) REFERENCES dbo.lm__question_types(QuestionTypeId),
    CONSTRAINT FK_lm__questions_SkillTypeId FOREIGN KEY (SkillTypeId) REFERENCES dbo.lm__skill_types(SkillTypeId),

    CONSTRAINT CK_lm__questions_Points CHECK (Points >= 0)
);

CREATE TABLE dbo.lm__answers
(
    AnswerId INT IDENTITY(1,1) NOT NULL,
    QuestionId INT NOT NULL,
    AnswerText NVARCHAR(500) NOT NULL,
    IsCorrect BIT NOT NULL,
    AnswerOrder INT NOT NULL,

    CONSTRAINT PK_lm__answers PRIMARY KEY (AnswerId),
    CONSTRAINT UQ_lm__answers UNIQUE (QuestionId, AnswerOrder),

    CONSTRAINT FK_lm__answers_QuestionId FOREIGN KEY (QuestionId) REFERENCES dbo.lm__questions(QuestionId)
);

/* Tesztek */

CREATE TABLE dbo.lm__tests
(
    TestId INT IDENTITY(1,1) NOT NULL,
    TestName NVARCHAR(150) NOT NULL,
    Description NVARCHAR(2000),
    LanguageId INT NOT NULL,
    TotalPoints INT NOT NULL,
    IsRandom BIT NOT NULL CONSTRAINT DF_lm__tests_IsRandom DEFAULT 0,
    IsActive BIT NOT NULL CONSTRAINT DF_lm__tests_IsActive DEFAULT 1,

    CONSTRAINT PK_lm__tests PRIMARY KEY (TestId),

    CONSTRAINT FK_lm__tests_LanguageId FOREIGN KEY (LanguageId) REFERENCES dbo.lm__languages(LanguageId),
    CONSTRAINT CK_lm__tests_TotalPoints CHECK (TotalPoints >= 0)
);

CREATE TABLE dbo.lm__test_questions
(
    TestQuestionId INT IDENTITY(1,1) NOT NULL,
    TestId INT NOT NULL,
    QuestionId INT NOT NULL,
    QuestionOrder INT NOT NULL,

    CONSTRAINT PK_lm__test_questions PRIMARY KEY (TestQuestionId),
    CONSTRAINT UQ_lm__test_questions UNIQUE (TestId, QuestionOrder),
    CONSTRAINT UQ_lm__test_questions_Test_Question UNIQUE (TestId, QuestionId),

    CONSTRAINT FK_lm__test_questions_TestId FOREIGN KEY (TestId) REFERENCES dbo.lm__tests(TestId),
    CONSTRAINT FK_lm__test_questions_QuestionId FOREIGN KEY (QuestionId) REFERENCES dbo.lm__questions(QuestionId)
);

/* Session */

CREATE TABLE dbo.lm__assessment_sessions
(
    AssessmentSessionId INT IDENTITY(1,1) NOT NULL,
    AssessmentModeId INT NOT NULL,
    LanguageId INT NOT NULL,
    SecondaryLanguageId INT NULL,
    SelectedLevelId INT NULL,
    SessionKey UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_lm__assessment_sessions_SessionKey DEFAULT NEWID(),
    PaceTypeId INT NULL,
    UserId INT NULL,
    NeedLevelTest BIT NOT NULL CONSTRAINT DF_lm__assessment_sessions_NeedLevelTest DEFAULT 0,
    Status NVARCHAR(50) NOT NULL,
    FinalLevelId INT NULL,
    CreatedOn DATETIME2 NOT NULL CONSTRAINT DF_lm__assessment_sessions_CreatedOn DEFAULT SYSUTCDATETIME(),
    CompletedOn DATETIME2 NULL,

    CONSTRAINT PK_lm__assessment_sessions PRIMARY KEY (AssessmentSessionId),
    CONSTRAINT UQ_lm__assessment_sessions UNIQUE (SessionKey),

    CONSTRAINT FK_lm__assessment_sessions_AssessmentModeId FOREIGN KEY (AssessmentModeId) REFERENCES dbo.lm__assessment_modes(AssessmentModeId),
    CONSTRAINT FK_lm__assessment_sessions_LanguageId FOREIGN KEY (LanguageId) REFERENCES dbo.lm__languages(LanguageId),
    CONSTRAINT FK_lm__assessment_sessions_SecondaryLanguageId FOREIGN KEY (SecondaryLanguageId) REFERENCES dbo.lm__languages(LanguageId),
    CONSTRAINT FK_lm__assessment_sessions_SelectedLevelId FOREIGN KEY (SelectedLevelId) REFERENCES dbo.lm__question_levels(QuestionLevelId),
    CONSTRAINT FK_lm__assessment_sessions_FinalLevelId FOREIGN KEY (FinalLevelId) REFERENCES dbo.lm__question_levels(QuestionLevelId),
    CONSTRAINT FK_lm__assessment_sessions_PaceTypeId FOREIGN KEY (PaceTypeId) REFERENCES dbo.lm__pace_types(PaceTypeId),

    CONSTRAINT CK_lm__assessment_sessions_Status
    CHECK (Status IN ('Started', 'Completed', 'Abandoned'))
);

CREATE TABLE dbo.lm__assessment_session_skills
(
    AssessmentSessionSkillId INT IDENTITY(1,1) NOT NULL,
    AssessmentSessionId INT NOT NULL,
    SkillTypeId INT NOT NULL,

    CONSTRAINT PK_lm__assessment_session_skills PRIMARY KEY (AssessmentSessionSkillId),
    CONSTRAINT UQ_lm__session_skill UNIQUE (AssessmentSessionId, SkillTypeId),

    CONSTRAINT FK_lm__assessment_session_skills_AssessmentSessionId FOREIGN KEY (AssessmentSessionId) REFERENCES dbo.lm__assessment_sessions(AssessmentSessionId),
    CONSTRAINT FK_lm__assessment_session_skills_SkillTypeId FOREIGN KEY (SkillTypeId) REFERENCES dbo.lm__skill_types(SkillTypeId)
);

/* Test attempts */

CREATE TABLE dbo.lm__test_attempts
(
    TestAttemptId INT IDENTITY(1,1) NOT NULL,
    AssessmentSessionId INT NULL,
    TestId INT NOT NULL,
    UserId INT NULL,
    TotalScore INT NOT NULL CONSTRAINT DF_lm__test_attempts_TotalScore DEFAULT 0,
    A1CorrectCount INT NOT NULL CONSTRAINT DF_lm__test_attempts_A1CorrectCount DEFAULT 0,
    A2CorrectCount INT NOT NULL CONSTRAINT DF_lm__test_attempts_A2CorrectCount DEFAULT 0,
    B1CorrectCount INT NOT NULL CONSTRAINT DF_lm__test_attempts_B1CorrectCount DEFAULT 0,
    B2CorrectCount INT NOT NULL CONSTRAINT DF_lm__test_attempts_B2CorrectCount DEFAULT 0,
    C1CorrectCount INT NOT NULL CONSTRAINT DF_lm__test_attempts_C1CorrectCount DEFAULT 0,
    ResultLevelId INT NULL,
    StartedOn DATETIME2 NOT NULL CONSTRAINT DF_lm__test_attempts_StartedOn DEFAULT SYSUTCDATETIME(),
    CompletedOn DATETIME2 NULL,
    IsCompleted BIT NOT NULL CONSTRAINT DF_lm__test_attempts_IsCompleted DEFAULT 0,
    AttemptNumber INT NOT NULL CONSTRAINT DF_lm__test_attempts_AttemptNumber DEFAULT 1,

    CONSTRAINT PK_lm__test_attempts PRIMARY KEY (TestAttemptId),

    CONSTRAINT FK_lm__test_attempts_AssessmentSessionId FOREIGN KEY (AssessmentSessionId) REFERENCES dbo.lm__assessment_sessions(AssessmentSessionId),
    CONSTRAINT FK_lm__test_attempts_TestId FOREIGN KEY (TestId) REFERENCES dbo.lm__tests(TestId),
    CONSTRAINT FK_lm__test_attempts_ResultLevelId FOREIGN KEY (ResultLevelId) REFERENCES dbo.lm__question_levels(QuestionLevelId),

    CONSTRAINT CK_lm__test_attempts_TotalScore CHECK (TotalScore >= 0),
    CONSTRAINT CK_lm__test_attempts_A1 CHECK (A1CorrectCount >= 0),
    CONSTRAINT CK_lm__test_attempts_A2 CHECK (A2CorrectCount >= 0),
    CONSTRAINT CK_lm__test_attempts_B1 CHECK (B1CorrectCount >= 0),
    CONSTRAINT CK_lm__test_attempts_B2 CHECK (B2CorrectCount >= 0),
    CONSTRAINT CK_lm__test_attempts_C1 CHECK (C1CorrectCount >= 0),
    CONSTRAINT CK_lm__test_attempts_AttemptNumber CHECK (AttemptNumber >= 1)
);

CREATE TABLE dbo.lm__test_attempt_answers
(
    TestAttemptAnswerId INT IDENTITY(1,1) NOT NULL,
    TestAttemptId INT NOT NULL,
    QuestionId INT NOT NULL,
    SelectedAnswerId INT NULL,
    IsCorrect BIT NOT NULL,
    EarnedPoints INT NOT NULL CONSTRAINT DF_lm__test_attempt_answers_EarnedPoints DEFAULT 0,
    AnsweredOn DATETIME2 NOT NULL CONSTRAINT DF_lm__test_attempt_answers_AnsweredOn DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_lm__test_attempt_answers PRIMARY KEY (TestAttemptAnswerId),
    CONSTRAINT UQ_lm__attempt_question UNIQUE (TestAttemptId, QuestionId),

    CONSTRAINT FK_lm__test_attempt_answers_TestAttemptId FOREIGN KEY (TestAttemptId) REFERENCES dbo.lm__test_attempts(TestAttemptId),
    CONSTRAINT FK_lm__test_attempt_answers_QuestionId FOREIGN KEY (QuestionId) REFERENCES dbo.lm__questions(QuestionId),
    CONSTRAINT FK_lm__test_attempt_answers_SelectedAnswerId FOREIGN KEY (SelectedAnswerId) REFERENCES dbo.lm__answers(AnswerId),

    CONSTRAINT CK_lm__test_attempt_answers_EarnedPoints CHECK (EarnedPoints >= 0)
);

/* Recommendation */

CREATE TABLE dbo.lm__recommendation_rules
(
    RecommendationRuleId INT IDENTITY(1,1) NOT NULL,
    LanguageId INT NOT NULL,
    QuestionLevelId INT NOT NULL,
    SkillTypeId INT NOT NULL,
    SecondaryLanguageId INT NULL,
    PaceTypeId INT NOT NULL,
    Priority INT NOT NULL,
    MatchMode NVARCHAR(50) NOT NULL,
    HotcakesProductSKU NVARCHAR(100) NULL,
    HotcakesCategoryKey NVARCHAR(100) NULL,
    HotcakesTag NVARCHAR(100) NULL,
    MinProducts INT NOT NULL,
    MaxProducts INT NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_lm__recommendation_rules_IsActive DEFAULT 1,

    CONSTRAINT PK_lm__recommendation_rules PRIMARY KEY (RecommendationRuleId),
    
    
    CONSTRAINT FK_lm__recommendation_rules_LanguageId FOREIGN KEY (LanguageId) REFERENCES dbo.lm__languages(LanguageId),
    CONSTRAINT FK_lm__recommendation_rules_QuestionLevelId FOREIGN KEY (QuestionLevelId) REFERENCES dbo.lm__question_levels(QuestionLevelId),
    CONSTRAINT FK_lm__recommendation_rules_SkillTypeId FOREIGN KEY (SkillTypeId) REFERENCES dbo.lm__skill_types(SkillTypeId),
    CONSTRAINT FK_lm__recommendation_rules_SecondaryLanguageId FOREIGN KEY (SecondaryLanguageId) REFERENCES dbo.lm__languages(LanguageId),
    CONSTRAINT FK_lm__recommendation_rules_PaceTypeId FOREIGN KEY (PaceTypeId) REFERENCES dbo.lm__pace_types(PaceTypeId),

    CONSTRAINT CK_lm__recommendation_rules_Priority CHECK (Priority >= 1),
    CONSTRAINT CK_lm__recommendation_rules_ProductRange
    CHECK (MinProducts >= 0 AND MaxProducts >= 0 AND MinProducts <= MaxProducts),
    CONSTRAINT CK_lm__recommendation_rules_MatchMode
    CHECK (MatchMode IN ('EXACT', 'FALLBACK', 'GENERAL'))
);

CREATE TABLE dbo.lm__recommendation_results
(
    RecommendationResultId INT IDENTITY(1,1) NOT NULL,
    AssessmentSessionId INT NOT NULL,
    SourceType NVARCHAR(50) NOT NULL,
    RecommendedLevelId INT NULL,
    RecommendationSummary NVARCHAR(2000),
    MinProducts INT NOT NULL,
    MaxProducts INT NOT NULL,
    CreatedOn DATETIME2 NOT NULL CONSTRAINT DF_lm__recommendation_results_CreatedOn DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_lm__recommendation_results PRIMARY KEY (RecommendationResultId),
    
    CONSTRAINT FK_lm__recommendation_results_AssessmentSessionId FOREIGN KEY (AssessmentSessionId) REFERENCES dbo.lm__assessment_sessions(AssessmentSessionId),
    CONSTRAINT FK_lm__recommendation_results_RecommendedLevelId FOREIGN KEY (RecommendedLevelId) REFERENCES dbo.lm__question_levels(QuestionLevelId),
    
    CONSTRAINT CK_lm__recommendation_results_ProductRange
    CHECK (MinProducts >= 0 AND MaxProducts >= 0 AND MinProducts <= MaxProducts),
    CONSTRAINT CK_lm__recommendation_results_SourceType
    CHECK (SourceType IN ('StudyGuide', 'LevelTest'))
);

CREATE TABLE dbo.lm__recommendation_result_items
(
    RecommendationResultItemId INT IDENTITY(1,1) NOT NULL,
    RecommendationResultId INT NOT NULL,
    ProductSKU NVARCHAR(100) NOT NULL,
    SortOrder INT NOT NULL,
    SourceRule NVARCHAR(100),
    SourceType NVARCHAR(50) NOT NULL,
    Score DECIMAL(10,2),
    IsPrimary BIT NOT NULL CONSTRAINT DF_lm__recommendation_result_items_IsPrimary DEFAULT 0,
    CreatedOn DATETIME2 NOT NULL CONSTRAINT DF_lm__recommendation_result_items_CreatedOn DEFAULT SYSUTCDATETIME(),


    CONSTRAINT PK_lm__recommendation_result_items PRIMARY KEY (RecommendationResultItemId),
    CONSTRAINT UQ_lm__result_sort UNIQUE (RecommendationResultId, SortOrder),
    CONSTRAINT UQ_lm__result_item_sku UNIQUE (RecommendationResultId, ProductSKU),

    CONSTRAINT FK_lm__recommendation_result_items_RecommendationResultId FOREIGN KEY (RecommendationResultId) REFERENCES dbo.lm__recommendation_results(RecommendationResultId),

    CONSTRAINT CK_lm__recommendation_result_items_SourceType
    CHECK (SourceType IN ('Rule', 'Fallback', 'Manual')),
    CONSTRAINT CK_lm__recommendation_result_items_SortOrder CHECK (SortOrder >= 1)
);

/* Indexek */

CREATE INDEX IX_lm__questions_Language_Level
    ON dbo.lm__questions (LanguageId, QuestionLevelId);

CREATE INDEX IX_lm__test_questions_Test
    ON dbo.lm__test_questions (TestId);

CREATE INDEX IX_lm__test_attempt_answers_Attempt
    ON dbo.lm__test_attempt_answers (TestAttemptId);

CREATE INDEX IX_lm__recommendation_rules_Filter
    ON dbo.lm__recommendation_rules
       (LanguageId, QuestionLevelId, SkillTypeId, PaceTypeId, IsActive);

CREATE INDEX IX_lm__recommendation_results_Session
    ON dbo.lm__recommendation_results (AssessmentSessionId);


CREATE UNIQUE INDEX UQ_lm__attempt_session
    ON dbo.lm__test_attempts (AssessmentSessionId, AttemptNumber)
    WHERE AssessmentSessionId IS NOT NULL;
