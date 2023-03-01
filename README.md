# dotnet-webapi-custom-mimetypes
A .Net WebAPI example that accepts and produces custom vendor mime types as a means of versioning or specializing views on a resource aka the **Re** (representation) in **ReST**. Swagger documentation is included.

Happily, JSON serialization is automatically detected for an Accept mime type like `application/vnd.custom+json;version=1`.

`dynamic` parameters may at times be required on POST/PUT/PATCH thus requiring manual deserialization. It is a bit more work but not too bad.

The examples Swaggers presents take an enormous amount of work. Also, 
examples and corresponding mime types are not tied meaning it is not clear that both must
match when executing test calls.

<img width="1451" alt="image" src="https://user-images.githubusercontent.com/148119/222008937-ffa13cd8-ed3b-4d8d-a03c-e120f1e76bd0.png">

...but it all works ok. There are likely refinements I'm not seeing.
