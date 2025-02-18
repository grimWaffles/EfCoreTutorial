IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Grades] (
    [GradeId] int NOT NULL IDENTITY,
    [GradeName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Grades] PRIMARY KEY ([GradeId])
);

CREATE TABLE [Students] (
    [StudentId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [GradeId] int NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([StudentId]),
    CONSTRAINT [FK_Students_Grades_GradeId] FOREIGN KEY ([GradeId]) REFERENCES [Grades] ([GradeId]) ON DELETE CASCADE
);

CREATE INDEX [IX_Students_GradeId] ON [Students] ([GradeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250211073354_IntialCreate', N'9.0.1');

ALTER TABLE [Students] ADD [DateOfBirth] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [Students] ADD [Height] decimal(18,2) NOT NULL DEFAULT 0.0;

ALTER TABLE [Students] ADD [PhotoUrl] varbinary(max) NOT NULL DEFAULT 0x;

ALTER TABLE [Students] ADD [Weight] real NOT NULL DEFAULT CAST(0 AS real);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250211073929_StudentEntityModification#1', N'9.0.1');

COMMIT;
GO

