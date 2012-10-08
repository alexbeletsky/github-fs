#Github File System for .NET

This is Readme-Driven-Development project aims for creating of convinient way of working with Git (Github) remote repository as file system.

##.NET Framework supported

4.0 and 4.5

##Sync and Async

Filesystem works both in 'sync' and 'async' ways. API's are identical, differs only 'Async' suffix of API methods and (maybe) number of arguments.

##Code samples and API

Initialization:

    var credentials = new Credentials
                          {
                              Username = "alexanderbeletsky",
                              Password = "mysecretcode"
                          };
    var fileSystem = new Github.FileSystem.Open("https://github.com/alexanderbeletsky/github-fs.net",
                                                credentials);

Check if file exist:

    if (fileSystem.FileExists("README.md"))
    {
        // use file
    }

Check if directory exist:

    if (fileSystem.DirectoryExists("src"))
    {
        // use directory
    }

File stat:

    var stats = fileSystem.Stat("README.md");
    System.Console.WriteLine("Created at: " + stats.CreatedAt);
    System.Console.WriteLine("Last change at: " + stats.LastChangeAt);
    System.Console.WriteLine("Author: " + stats.Author);
    System.Console.WriteLine("Last change by: " + stats.LastChangeBy);

Getting the list of file system objects:

    var fileSystemObjects = fileSystem.GetSystemObjects();
    foreach (var o in fileSystemObjects)
    {
        if (o is Github.Directory)
        {
            System.Console.WriteLine("Catch a directory:" + o.Path);
        }
        else if (o is Github.File)
        {
            System.Console.WriteLine("Catch a file: " + o.Path);
        }
    }

Reading the files:

    var file = fileSystem.OpenFile("README.md");
    var content = file.Read().AsString();

Writing the files:

    var hello = "Hello, world";
    file.Write(hello.AsBytes());

Rename a file:

    file.Rename("README_2.md");

Delete a file:

    file.Delete();

##Details of work

Library will use [Github API v3](http://developer.github.com/v3/). As for HTTP operations, there are several options:

* [WebClient](http://msdn.microsoft.com/en-us/library/system.net.webclient.aspx) -  very simple, well known class, supporting sync/async but in old fashion.
* [EasyHttp](https://github.com/hhariri/EasyHttp) - nice API, works great with JSON payload, but looks like only sync operations.
* [HttpClient](http://msdn.microsoft.com/en-us/library/system.net.http.httpclient.aspx) - modern and promising, with support of Task<T>, but never used used. Is it only .NET 4.5?