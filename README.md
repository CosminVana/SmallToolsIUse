# SmallToolsIUse
A Repository with small tools I use during my work

## FolderDiff
Compares the structure of 2 directories and prints the following differences:
- Files existing only in source
- Files existing only in target
- Files existing in both directories, but of different size
- Folders existing only in source
- Folders existing only in destination

Please note that some files may have the same size, but different content. They will not be returned as differences (e.g. Bitmap images will have fixed file size based on image size, regardless of it's content, Text files will have the same size if they have the same number of characters). No further enhancements are planned to address file content differences, but it can be easily implemented in FolderDiff class.

### Usage
<code>SmallToolsIUse.FolderDiff.exe "C:\Some Folder" "C:\Some other folder"</code>

### Release
[Go to release page](https://github.com/CosminVana/SmallToolsIUse/releases/tag/FolderDiff)

## SmallToolsIUse.Linq.Extensions
A set of Linq extension methods which can be used to solve common scenarios in .NET Applications.

Add the following using statement:

<code>using SmallToolsIUse.Linq.Extensions;</code>

### IEnumerable Extensions

Check if an instance which implementes IEnumerable is null or empty:

<code>if(!customers.IsNullOrEmpty())
  {
      // do something with customers
  }</code>

Compare two IEnumerable of different types (e.g. DTO from your API and entities from your database):

<code>var customersToUpdate = customersDto.In(customersFromDb, c => c.Id, c => c.Id)</code>

<code>var customersToDelete = customersFromDb.NotIn(customersDto, c => c.Id, c => c.Id)</code>

<code>var customersToInsert = customersDto.NotIn(customersFromDb, c => c.Id, c => c.Id)</code>

Note: If both collections have the same type, you need to specify a single key selector.

### IQueryable Extensions

Add sorting to your IQueryable using the field name. 

Without extensions:

<code>// assume we have sortBy field as a string, as it may be the case if it's captured from the query string.
var myCustomers = dbContext.Customers.Select(c => new CustomerDto());
if(sortBy == "FirstName")
{
  myCustomers = myCustomers.OrderBy(c => c.FirstName); // also, need to check if it's descending or not
}
else if(sortBy == "LastName"
{
   myCustomers = myCustomers.OrderBy(c => c.LastName); // also, need to check if it's descending or not
}</code>

With linq extensions:

<code>// assume we have sortBy field as a string, as it may be the case if it's captured from the query string.
var myCustomers = dbContext.Customers.Select(c => new CustomerDto());
myCustomers = myCustomers.OrderByField(sortBy, orderDirection);</code>

You have the following methods which will be properly translated to SQL:

<code>OrderByField, ThenByField, Paginate, SortAndPaginate</code>

### Releases
https://www.nuget.org/packages/SmallToolsIUse.Linq.Extensions/ 


