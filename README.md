# odata
Samples for the OData series of posts: https://weblogs.asp.net/ricardoperes/asp-net-core-odata-part-1

When running the 'odata.server' project it will create the database, but you will need to populate it yourself.

The following URLs are available:

Blogs:

GET https://localhost:5001/odata/blogs

GET https://localhost:5001/odata/blogs(1) (once the database is populated)

GET https://localhost:5001/odata/blogs/FindByCreation(date=2009-08-01) (replace with some existing date)

GET https://localhost:5001/odata/blogs/CountPosts(id=1) (once the database is populated)

POST https://localhost:5001/odata/blogs/Blog(1)/Count (once the database is populated)

POST https://localhost:5001/odata/blogs(1)/Update (once the database is populated)



BlogPosts:

GET https://localhost:5001/odata/CountBlogPosts()


Parents:

GET https://localhost:5001/odata/Parents (once the database is populated)

GET https://localhost:5001/odata/Parents(1) (once the database is populated)

