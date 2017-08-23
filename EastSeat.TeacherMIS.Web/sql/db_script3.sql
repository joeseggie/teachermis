IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Email] nvarchar(256),
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset,
    [NormalizedEmail] nvarchar(256),
    [NormalizedUserName] nvarchar(256),
    [PasswordHash] nvarchar(max),
    [PhoneNumber] nvarchar(max),
    [PhoneNumberConfirmed] bit NOT NULL,
    [SecurityStamp] nvarchar(max),
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256),
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Name] nvarchar(256),
    [NormalizedName] nvarchar(256),
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max),
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max),
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170809140926_InitialSchema', N'1.1.2');

GO

CREATE TABLE [School] (
    [SchoolId] uniqueidentifier NOT NULL,
    [Name] varchar(200) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_School] PRIMARY KEY ([SchoolId])
);

GO

CREATE TABLE [Teachers] (
    [TeacherId] uniqueidentifier NOT NULL,
    [ConfirmationEscMinute] varchar(200) NOT NULL,
    [CurrentPosition] varchar(100) NOT NULL,
    [CurrentPositionAppMinute] varchar(200) NOT NULL,
    [CurrentPositionPostingDate] date NOT NULL,
    [DateOfBirth] date NOT NULL,
    [FirstAppEscMinute] varchar(200) NOT NULL,
    [FirstProbationAppDate] date NOT NULL,
    [Fullname] varchar(50) NOT NULL,
    [Gender] char(10) NOT NULL,
    [IppsNumber] varchar(20) NOT NULL,
    [ProbationAppDesignation] varchar(100) NOT NULL,
    [RegistrationNumber] varchar(20) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [SchoolId] uniqueidentifier NOT NULL,
    [UtsFileNumber] varchar(20) NOT NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY ([TeacherId]),
    CONSTRAINT [FK_Teachers_School_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [School] ([SchoolId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Headmaster] (
    [HeadmasterId] uniqueidentifier NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [SchoolId] uniqueidentifier NOT NULL,
    [TeacherId] uniqueidentifier NOT NULL,
    [TeacherId1] uniqueidentifier,
    CONSTRAINT [PK_Headmaster] PRIMARY KEY ([HeadmasterId]),
    CONSTRAINT [FK_Headmaster_School_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [School] ([SchoolId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Headmaster_Teachers_TeacherId1] FOREIGN KEY ([TeacherId1]) REFERENCES [Teachers] ([TeacherId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Headmaster_SchoolId] ON [Headmaster] ([SchoolId]);

GO

CREATE UNIQUE INDEX [IX_Headmaster_TeacherId1] ON [Headmaster] ([TeacherId1]) WHERE [TeacherId1] IS NOT NULL;

GO

CREATE INDEX [IX_Headmaster_TeacherId_SchoolId] ON [Headmaster] ([TeacherId], [SchoolId]);

GO

CREATE INDEX [IX_Teachers_SchoolId] ON [Teachers] ([SchoolId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170810134231_SchoolHeadmasterTeacher', N'1.1.2');

GO

ALTER TABLE [Headmaster] DROP CONSTRAINT [FK_Headmaster_Teachers_TeacherId1];

GO

DROP INDEX [IX_Headmaster_TeacherId1] ON [Headmaster];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Headmaster') AND [c].[name] = N'TeacherId1');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Headmaster] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Headmaster] DROP COLUMN [TeacherId1];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Headmaster') AND [c].[name] = N'TeacherId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Headmaster] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Headmaster] ALTER COLUMN [TeacherId] uniqueidentifier;

GO

CREATE UNIQUE INDEX [IX_Headmaster_TeacherId] ON [Headmaster] ([TeacherId]) WHERE [TeacherId] IS NOT NULL;

GO

ALTER TABLE [Headmaster] ADD CONSTRAINT [FK_Headmaster_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([TeacherId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170810135348_HeadmasterTeacherRelation', N'1.1.2');

GO

CREATE TABLE [Subject] (
    [SubjectId] uniqueidentifier NOT NULL,
    [Description] varchar(200) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Subject] PRIMARY KEY ([SubjectId])
);

GO

CREATE TABLE [TeacherFile] (
    [TeacherFileId] uniqueidentifier NOT NULL,
    [Details] varchar(500) NOT NULL,
    [RecordDate] date NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [TeacherId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_TeacherFile] PRIMARY KEY ([TeacherFileId]),
    CONSTRAINT [FK_TeacherFile_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([TeacherId]) ON DELETE CASCADE
);

GO

CREATE TABLE [SubjectTaught] (
    [SubjectTaughtId] uniqueidentifier NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [SubjectId] uniqueidentifier NOT NULL,
    [TeacherId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_SubjectTaught] PRIMARY KEY ([SubjectTaughtId]),
    CONSTRAINT [FK_SubjectTaught_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([SubjectId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SubjectTaught_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([TeacherId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_SubjectTaught_SubjectId] ON [SubjectTaught] ([SubjectId]);

GO

CREATE INDEX [IX_SubjectTaught_TeacherId] ON [SubjectTaught] ([TeacherId]);

GO

CREATE UNIQUE INDEX [IX_TeacherFile_TeacherId] ON [TeacherFile] ([TeacherId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170810193027_TeacherFileSubjectTaught', N'1.1.2');

GO

ALTER TABLE [Headmaster] DROP CONSTRAINT [FK_Headmaster_Teachers_TeacherId];

GO

ALTER TABLE [SubjectTaught] DROP CONSTRAINT [FK_SubjectTaught_Teachers_TeacherId];

GO

ALTER TABLE [Teachers] DROP CONSTRAINT [FK_Teachers_School_SchoolId];

GO

ALTER TABLE [TeacherFile] DROP CONSTRAINT [FK_TeacherFile_Teachers_TeacherId];

GO

ALTER TABLE [Teachers] DROP CONSTRAINT [PK_Teachers];

GO

EXEC sp_rename N'Teachers', N'Table';

GO

EXEC sp_rename N'Table.IX_Teachers_SchoolId', N'IX_Table_SchoolId', N'INDEX';

GO

ALTER TABLE [Table] ADD CONSTRAINT [PK_Table] PRIMARY KEY ([TeacherId]);

GO

ALTER TABLE [Headmaster] ADD CONSTRAINT [FK_Headmaster_Table_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Table] ([TeacherId]) ON DELETE NO ACTION;

GO

ALTER TABLE [SubjectTaught] ADD CONSTRAINT [FK_SubjectTaught_Table_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Table] ([TeacherId]) ON DELETE CASCADE;

GO

ALTER TABLE [Table] ADD CONSTRAINT [FK_Table_School_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [School] ([SchoolId]) ON DELETE CASCADE;

GO

ALTER TABLE [TeacherFile] ADD CONSTRAINT [FK_TeacherFile_Table_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Table] ([TeacherId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170810193815_TeacherTableDefinition', N'1.1.2');

GO

ALTER TABLE [Headmaster] DROP CONSTRAINT [FK_Headmaster_Table_TeacherId];

GO

ALTER TABLE [SubjectTaught] DROP CONSTRAINT [FK_SubjectTaught_Table_TeacherId];

GO

ALTER TABLE [Table] DROP CONSTRAINT [FK_Table_School_SchoolId];

GO

ALTER TABLE [TeacherFile] DROP CONSTRAINT [FK_TeacherFile_Table_TeacherId];

GO

ALTER TABLE [Table] DROP CONSTRAINT [PK_Table];

GO

EXEC sp_rename N'Table', N'Teacher';

GO

EXEC sp_rename N'Teacher.IX_Table_SchoolId', N'IX_Teacher_SchoolId', N'INDEX';

GO

ALTER TABLE [Teacher] ADD CONSTRAINT [PK_Teacher] PRIMARY KEY ([TeacherId]);

GO

ALTER TABLE [Headmaster] ADD CONSTRAINT [FK_Headmaster_Teacher_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teacher] ([TeacherId]) ON DELETE NO ACTION;

GO

ALTER TABLE [SubjectTaught] ADD CONSTRAINT [FK_SubjectTaught_Teacher_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teacher] ([TeacherId]) ON DELETE CASCADE;

GO

ALTER TABLE [Teacher] ADD CONSTRAINT [FK_Teacher_School_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [School] ([SchoolId]) ON DELETE CASCADE;

GO

ALTER TABLE [TeacherFile] ADD CONSTRAINT [FK_TeacherFile_Teacher_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teacher] ([TeacherId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170810194105_TeacherTableRename', N'1.1.2');

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'TeacherFile') AND [c].[name] = N'Details');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [TeacherFile] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [TeacherFile] ALTER COLUMN [Details] varchar(2000) NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170813171535_TeacherFileDetails', N'1.1.2');

GO

ALTER TABLE [AspNetUsers] ADD [Firstname] nvarchar(50) NOT NULL DEFAULT N'';

GO

ALTER TABLE [AspNetUsers] ADD [Lastname] nvarchar(50) NOT NULL DEFAULT N'';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170813202612_AppUserFirstAndLastname', N'1.1.2');

GO

ALTER TABLE [Subject] ADD [SubjectCategoryId] uniqueidentifier;

GO

CREATE TABLE [SubjectCategory] (
    [SubjectCategoryId] uniqueidentifier NOT NULL,
    [Description] varchar(50) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_SubjectCategory] PRIMARY KEY ([SubjectCategoryId])
);

GO

CREATE INDEX [IX_Subject_SubjectCategoryId] ON [Subject] ([SubjectCategoryId]);

GO

ALTER TABLE [Subject] ADD CONSTRAINT [FK_Subject_SubjectCategory_SubjectCategoryId] FOREIGN KEY ([SubjectCategoryId]) REFERENCES [SubjectCategory] ([SubjectCategoryId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170815063215_SubjectCategoryTable', N'1.1.2');

GO

ALTER TABLE [SubjectCategory] ADD [Stub] varchar(50) NOT NULL DEFAULT N'';

GO

CREATE INDEX [IX_SubjectCategory_Stub] ON [SubjectCategory] ([Stub]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170815064318_SubjectCategoryStub', N'1.1.2');

GO

DROP INDEX [IX_TeacherFile_TeacherId] ON [TeacherFile];

GO

CREATE INDEX [IX_TeacherFile_TeacherId] ON [TeacherFile] ([TeacherId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170819124134_ChangeTeacherFileRelation', N'1.1.2');

GO

ALTER TABLE [School] ADD [DistrictId] uniqueidentifier;

GO

CREATE TABLE [District] (
    [DistrictId] uniqueidentifier NOT NULL,
    [Name] varchar(50) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_District] PRIMARY KEY ([DistrictId])
);

GO

CREATE INDEX [IX_School_DistrictId] ON [School] ([DistrictId]);

GO

ALTER TABLE [School] ADD CONSTRAINT [FK_School_District_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [District] ([DistrictId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170821080034_FeatureDistricts', N'1.1.2');

GO

ALTER TABLE [School] ADD [SchoolCategoryId] uniqueidentifier;

GO

CREATE TABLE [SchoolCategory] (
    [SchoolCategoryId] uniqueidentifier NOT NULL,
    [Description] varchar(50) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_SchoolCategory] PRIMARY KEY ([SchoolCategoryId])
);

GO

CREATE INDEX [IX_School_SchoolCategoryId] ON [School] ([SchoolCategoryId]);

GO

ALTER TABLE [School] ADD CONSTRAINT [FK_School_SchoolCategory_SchoolCategoryId] FOREIGN KEY ([SchoolCategoryId]) REFERENCES [SchoolCategory] ([SchoolCategoryId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170821095426_SchoolCategory', N'1.1.2');

GO

ALTER TABLE [SubjectCategory] ADD [Salary] money;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170821122940_SubjectCategorySalary', N'1.1.2');

GO

ALTER TABLE [District] ADD [WageAllocation] money;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170821135615_DistrictWageAllocation', N'1.1.2');

GO

