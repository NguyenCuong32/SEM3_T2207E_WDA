CREATE TABLE [PROJECT] (
    [ProjectID] int NOT NULL IDENTITY,
    [ProjectKey] varchar(10) NOT NULL,
    [projectName] nvarchar(128) NOT NULL,
    [projectLead] varchar(128) NOT NULL,
    [userCrate] varchar(128) NOT NULL,
    CONSTRAINT [PK_PROJECT] PRIMARY KEY ([ProjectID])
);
GO


CREATE TABLE [USERS] (
    [username] varchar(128) NOT NULL,
    [password] nvarchar(512) NOT NULL,
    [email] nvarchar(128) NOT NULL,
    [active] bit NOT NULL DEFAULT (((1))),
    [ngayTao] datetime NOT NULL DEFAULT ((getdate())),
    [ngayCapNhat] datetime NOT NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__USERS__F3DBC573FE346B55] PRIMARY KEY ([username])
);
GO


CREATE UNIQUE INDEX [UQ__PROJECT__C048AC95F56A857D] ON [PROJECT] ([ProjectKey]);
GO


