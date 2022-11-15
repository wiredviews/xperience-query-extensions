# Xperience Query Extensions

[![GitHub Actions CI: Build](https://github.com/wiredviews/xperience-query-extensions/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/wiredviews/xperience-query-extensions/actions/workflows/ci.yml)

[![Publish Packages to NuGet](https://github.com/wiredviews/xperience-query-extensions/actions/workflows/publish.yml/badge.svg?branch=main)](https://github.com/wiredviews/xperience-query-extensions/actions/workflows/publish.yml)

[![NuGet Package](https://img.shields.io/nuget/v/XperienceCommunity.QueryExtensions.svg)](https://www.nuget.org/packages/XperienceCommunity.QueryExtensions)

This package provides a set of extension methods for Kentico Xperience 13.0 `DocumentQuery`, `MultiDocumentQuery`, `ObjectQuery`, and `IPageRetriever` [data access APIs](https://docs.xperience.io/13api/content-management/pages).

## Dependencies

This package is compatible with ASP.NET Core 3.1 -> ASP.NET Core 5 applications or libraries integrated with Kentico Xperience 13.0.

## How to Use?

1. Install the NuGet package in your ASP.NET Core project (or class library)

   ```bash
   dotnet add package XperienceCommunity.QueryExtensions
   ```

1. Add the correct `using` to have the extensions appear in intellisense

   `using XperienceCommunity.QueryExtensions.Documents;`

   `using XperienceCommunity.QueryExtensions.Objects;`

   `using XperienceCommunity.QueryExtensions.Collections;`

   > The extension methods are all in explicit namespaces to prevent conflicts with extensions that Xperience might add in the future or extensions that the developer might have already created.
   >
   > If you are using C# 10, you can apply these globally with [C# 10 implicit usings](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/overview#implicit-using-directives)

## Extension Methods

### DocumentQuery

#### Prerequisites

```csharp
using XperienceCommunity.QueryExtensions.Documents;
```

#### Examples

> These work for both `DocumentQuery<T>` and `MultiDocumentQuery`

```csharp
public void QueryDocument(Guid nodeGuid)
{
    var query = DocumentHelper.GetDocuments()
        .WhereNodeGUIDEquals(nodeGuid);
}
```

```csharp
public void QueryDocument(int nodeID)
{
    var query = DocumentHelper.GetDocuments()
        .WhereNodeIDEquals(nodeID);
}
```

```csharp
public void QueryDocument(int documentID)
{
    var query = DocumentHelper.GetDocuments()
        .WhereDocumentIDEquals(documentID);
}
```

```csharp
var query = DocumentHelper.GetDocuments()
    .OrderByNodeOrder();
```

```csharp
var query = DocumentHelper.GetDocuments()
    .Tap(q => 
    {
        // access the query 'q'
    });
```

```csharp
bool condition = ...

var query = DocumentHelper.GetDocuments()
    .If(condition, q => 
    {
        // when condition is true
    });
```

```csharp
bool condition = ...

var query = DocumentHelper.GetDocuments()
    .If(condition, 
    q => 
    {
        // when condition is true
    }, 
    q =>
    {
        // when condition is false
    });
```

```csharp
var query = DocumentHelper.GetDocuments()
    .OrderByDescending(nameof(TreeNode.NodeID))
    .TopN(1)
    .DebugQuery();

/*
--- BEGIN [path\to\your\app\Program.cs] QUERY ---


DECLARE @DocumentCulture nvarchar(max) = N'en-US';

SELECT TOP 1 *
FROM View_CMS_Tree_Joined AS V WITH (NOLOCK, NOEXPAND) LEFT OUTER JOIN COM_SKU AS S WITH (NOLOCK) ON [V].[NodeSKUID] = [S].[SKUID]
WHERE [DocumentCulture] = @DocumentCulture
ORDER BY NodeID DESC


--- END [path\to\your\app\Program.cs] QUERY ---
*/
```

```csharp
var query = DocumentHelper.GetDocuments()
    .OrderByDescending(nameof(TreeNode.NodeID))
    .TopN(1)
    .DebugQuery("Newest Document");

/*
--- BEGIN [Newest Document] QUERY ---


DECLARE @DocumentCulture nvarchar(max) = N'en-US';

SELECT TOP 1 *
FROM View_CMS_Tree_Joined AS V WITH (NOLOCK, NOEXPAND) LEFT OUTER JOIN COM_SKU AS S WITH (NOLOCK) ON [V].[NodeSKUID] = [S].[SKUID]
WHERE [DocumentCulture] = @DocumentCulture
ORDER BY NodeID DESC


--- END [Newest Document] QUERY ---
*/
```

```csharp
var query = DocumentHelper.GetDocuments()
    .OrderByDescending(nameof(TreeNode.NodeID))
    .TopN(1)
    .TapQueryText(fullQueryText =>
    {
        Debug.WriteLine(fullQueryText);
    })
    .WhereEquals(...)
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
var query = DocumentHelper.GetDocuments()
    .Where(w => w.WhereInPath("path1", "path2"));
```

### ObjectQuery

#### Prerequisites

```csharp
using XperienceCommunity.QueryExtensions.Objects;
```

#### Examples

```csharp
return UserInfo.Provider.Get()
    .Tap(q =>
    {
        // access the query
    });
```

```csharp
bool condition = ...

var query = UserInfo.Provider.Get()
    .If(condition, q => 
    {
        // when condition is true
    });
```

```csharp
bool condition = ...

var query = UserInfo.Provider.Get()
    .If(condition, 
    q => 
    {
        // when condition is true
    }, 
    q =>
    {
        // when condition is false
    });
```

```csharp
var query = UserInfo.Provider.Get()
    .OrderByDescending(nameof(UserInfo.UserLastModified))
    .TopN(1)
    .DebugQuery();

/*
--- BEGIN [path\to\your\app\Program.cs] QUERY ---


SELECT TOP 1 *
FROM CMS_User
ORDER BY UserLastModified DESC


--- END [path\to\your\app\Program.cs] QUERY ---
*/
```

```csharp
var query = UserInfo.Provider.Get()
    .OrderByDescending(nameof(UserInfo.UserLastModified))
    .TopN(1)
    .DebugQuery("User");

/*
--- QUERY [User] START ---


SELECT TOP 1 *
FROM CMS_User
ORDER BY UserLastModified DESC


--- QUERY [User] END ---
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
var query = UserInfo.Provider.Get()
    .TapQueryText(text =>
    {
        // do something with the query text
    });
```

```csharp
var query = UserInfo.Provider.Get()
    .Source(s => s.InnerJoin<UserSettingInfo>(
        "UserID", 
        "UserSettingUserID", 
        "MY_ALIAS",
        additionalCondition: new WhereCondition("MY_ALIAS.UserWaitingForApproval", QueryOperator.Equals, true),
        hints: new[] { SqlHints.NOLOCK }))
    .TopN(1)
    .DebugQuery("User");

/*
--- QUERY [User] START ---


SELECT TOP 1 *
FROM CMS_User
INNER JOIN CMS_UserSetting AS MY_ALIAS WITH (NOLOCK) ON UserID = MY_ALIAS.UserSettingUserID AND MY_ALIAS.UserWaitingForApproval = 1
ORDER BY UserLastModified DESC


--- QUERY [User] END ---
*/
```

```csharp
// ExecuteAsync returns a populated dataset with all the columns returned by the query.
// When there are no results, dataset.Tables[0] will still be populated with an empty DataTable.

var dataset = await UserInfo.Provider.Get()
    .Source(source => source
        .InnerJoin<UserSettingInfo>(
            "UserID", 
            "UserSettingUserID", 
            "MY_ALIAS")
        .InnerJoin<CustomerInfo>(
            "CustomerUserID",
            "UserID",
            "C",
            )
        )
    .Columns("UserID", "UserSettingID", "CustomerID")
    .ExecuteAsync();
    
foreach (var row in dataset.Tables[0].Rows)
{
    Console.WriteLine($"User: {row["UserID"]}, User Setting: {row["UserSettingID"]}, Customer: {row["CustomerID"]}");
}
```

### Collections

#### Requirements

```csharp
using XperienceCommunity.QueryExtensions.Collections;
```

#### Examples

```csharp
TreeNode? page = await retriever
    .RetrieveAsync<TreeNode>(q => q.TopN(1), cancellationToken: token)
    .FirstOrDefaultAsync();
```

```csharp
IList<TreeNode> pages = await retriever
    .RetrieveAsync<TreeNode>(cancellationToken: token)
    .ToListAsync();
```

```csharp
IList<TreeNode> pages = await retriever
    .RetrieveAsync<TreeNode>(cancellationToken: token)
    .ToArrayAsync();
```

### PageRetriever

#### Requirements

```csharp
using Kentico.Content.Web.Mvc;
```

#### Examples

```csharp
void GetPages(int pageIndex, int pageSize)
{
    var result = await retriever.RetrievePagedAsync<TreeNode>(
        pageIndex,
        pageSize,
        q => q.OrderByNodeOrder(),
        cancellationToken: token);

    int total = result.TotalRecords;
    List<TreeNode> pages = result.Items;

    // or

    var (totalRecords, pages) = await retriever.RetrievePagedAsync<TreeNode>(
        pageIndex,
        pageSize,
        q => q.OrderByNodeOrder(),
        cancellationToken: token);
}
```

### XperienceCommunityConnectionHelper

#### Examples

```csharp
var dataSet = await XperienceCommunityConnectionHelper.ExecuteQueryAsync("CMS.User", "GetAllUsersCustom");
```

```csharp
string queryText = @"
SELECT *
FROM CMS_User
WHERE UserID = @UserID
"

var queryParams = new QueryDataParameters
{
    { "UserID", 3 }
};

var dataSet = await XperienceCommunityConnectionHelper.ExecuteQueryAsync(queryText, queryParams, token: token);
```

## References

### .NET

- [Nullable reference types (C# reference)](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types)

### Kentico Xperience

- [Kentico Xperience 13 Beta 3 - New Data Access APIs](https://dev.to/seangwright/kentico-xperience-13-beta-3-new-data-access-apis-1oha)
- [Pages API Examples](https://docs.xperience.io/13api/content-management/pages)
- [Retrieving pages in custom scenarios](https://docs.xperience.io/custom-development/working-with-pages-in-the-api#WorkingwithpagesintheAPI-Retrievingpagesincustomscenarios)
- [Improvements under the hood – Document and ObjectQuery enumeration without DataSets](https://devnet.kentico.com/articles/improvements-under-the-hood-document-and-objectquery-enumeration-without-datasets)
