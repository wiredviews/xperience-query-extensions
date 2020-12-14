# Xperience Query Extensions

[![NuGet Package](https://img.shields.io/nuget/v/WiredViews.Xperience.QueryExtensions.svg)](https://www.nuget.org/packages/WiredViews.Xperience.QueryExtensions)

This package provides a set of extension methods for Kentico Xperience 13.0 `DocumentQuery`, `MultiDocumentQuery`, and `ObjectQuery` [data access APIs](https://docs.xperience.io/13api/content-management/pages).

## Dependencies

This package is compatible with ASP.NET Core 3.1 -> ASP.NET Core 5 applications or libraries integrated with Kentico Xperience 13.0.

## How to Use?

1. Install the NuGet package in your ASP.NET Core project (or class library)

   ```bash
   dotnet add package WiredViews.Xperience.QueryExtensions
   ```

1. The extension methods are all in the `CMS.DocumentEngine` and `CMS.DataEngine` namespaces, so assuming you have the package installed you should see them appear in Intellisense.

## Extension Method Examples

### DocumentQuery

```csharp
public void QueryDocument(Guid nodeGuid)
{
    var query = DocumentHelper.GetDocuments()
        .WhereNodeGUIDEquals(nodeGuid);
}
```

```csharp
var query = DocumentHelper.GetDocuments()
    .OrderByNodeOrder();
```

```csharp
var query = DocumentHelper.GetDocuments()
    .OrderByDescending(nameof(TreeNode.NodeID))
    .TopN(1)
    .DebugQuery();

/*
~~~ BEGIN [path\to\your\app\Program.cs] QUERY ~~~


DECLARE @DocumentCulture nvarchar(max) = N'en-US';

SELECT TOP 1 *
FROM View_CMS_Tree_Joined AS V WITH (NOLOCK, NOEXPAND) LEFT OUTER JOIN COM_SKU AS S WITH (NOLOCK) ON [V].[NodeSKUID] = [S].[SKUID]
WHERE [DocumentCulture] = @DocumentCulture
ORDER BY NodeID DESC


~~~ END [path\to\your\app\Program.cs] QUERY ~~~
*/
```

```csharp
var query = DocumentHelper.GetDocuments()
    .OrderByDescending(nameof(TreeNode.NodeID))
    .TopN(1)
    .DebugQuery("Newest Document");

/*
~~~ BEGIN [Newest Document] QUERY ~~~


DECLARE @DocumentCulture nvarchar(max) = N'en-US';

SELECT TOP 1 *
FROM View_CMS_Tree_Joined AS V WITH (NOLOCK, NOEXPAND) LEFT OUTER JOIN COM_SKU AS S WITH (NOLOCK) ON [V].[NodeSKUID] = [S].[SKUID]
WHERE [DocumentCulture] = @DocumentCulture
ORDER BY NodeID DESC


~~~ END [Newest Document] QUERY ~~~
*/
```

```csharp
public void QueryDatabase(ILogger logger)
{
    var query = DocumentHelper.GetDocuments()
        .OrderByDescending(nameof(TreeNode.NodeID))
        .TopN(1)
        .LogQuery(logger, "Logged Query");
}
```

```csharp
public async Task QueryDatabase(CancellationToken token)
{
    List<TreeNode> pages = await DocumentHelper.GetDocuments()
        .TopN(5)
        .ToListAsync(token);
}
```

```csharp
public async Task QueryDatabase(CancellationToken token)
{
    TreeNode? newestPage = await DocumentHelper.GetDocuments()
        .OrderByDescending(nameof(TreeNode.NodeID))
        .TopN(1)
        .FirstOrDefaultAsync(token);

    if (newestPage is null)
    {
        return;
    }

    // ...
}
```

### ObjectQuery

```csharp
var query = UserInfo.Provider.Get()
    .OrderByDescending(nameof(UserInfo.UserLastModified))
    .TopN(1)
    .DebugQuery();

/*
~~~ BEGIN [path\to\your\app\Program.cs] QUERY ~~~


SELECT TOP 1 *
FROM CMS_User
ORDER BY UserLastModified DESC


~~~ END [path\to\your\app\Program.cs] QUERY ~~~
*/
```

```csharp
var query = UserInfo.Provider.Get()
    .OrderByDescending(nameof(UserInfo.UserLastModified))
    .TopN(1)
    .DebugQuery("User");

/*
~~~ QUERY [User] START ~~~


SELECT TOP 1 *
FROM CMS_User
ORDER BY UserLastModified DESC


~~~ QUERY [User] END ~~~
*/
```

```csharp
public void QueryDatabase(ILogger logger)
{
    var query = UserInfo.Provider.Get()
        .OrderByDescending(nameof(UserInfo.UserLastModified))
        .TopN(1)
        .LogQuery(logger, "Logged User Query");
}
```

```csharp
public async Task QueryDatabase(CancellationToken token)
{
    List<UserInfo> recentlyUpdatedUsers = await UserInfo.Provider.Get()
        .OrderByDesc(nameof(UserInfo.UserLastModified))
        .TopN(10)
        .ToListAsync(token);
}
```

```csharp
public async Task QueryDatabase(CancellationToken token)
{
    UserInfo? user = await UserInfo.Provider.Get()
        .OrderByDesc(nameof(UserInfo.UserLastModified))
        .TopN(1)
        .FirstOrDefaultAsync(token);

    if (user is null)
    {
        return;
    }

    // ...
}
```

## References

### .NET

- [Nullable reference types (C# reference)](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types)

### Kentico Xperience

- [Kentico Xperience 13 Beta 3 - New Data Access APIs](https://dev.to/seangwright/kentico-xperience-13-beta-3-new-data-access-apis-1oha)
- [Pages API Examples](https://docs.xperience.io/13api/content-management/pages)
- [Retrieving pages in custom scenarios](https://docs.xperience.io/custom-development/working-with-pages-in-the-api#WorkingwithpagesintheAPI-Retrievingpagesincustomscenarios)
- [Improvements under the hood – Document and ObjectQuery enumeration without DataSets](https://devnet.kentico.com/articles/improvements-under-the-hood-document-and-objectquery-enumeration-without-datasets)
