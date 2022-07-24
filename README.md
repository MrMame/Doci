
Basic file indexing and search tool using Lucene Net v3 and Dotnet Framework 4.8.2


The intention for this project is to present my programming techniques/skills to other people which are interested to know.
Tests will be implemented, but without focusing on a wide code coverage.




## Release Dependencies
- .Net-Framework (v4.8.2)
- Lucene.Net (v3.0.3)
- TikaOnDotnet (v1.17.1)
- TikaOnDotnet.TextExtractor (v1.17.1)

## Testing Dependencies
- .Net Core (v3.1)
- NUnit (v3.12.0)
- NSubstitue (v4.3.0)

## Architecture
- Public interfaces for using an assembly are stored inside a similar named assembly, but with a ".Contracts" suffix. All dependencies for using the target assemblies are only going against this additional ".Contracts" assembly.  
- All Public Interfaces defined by the ".COntracts" assemblies are the main entry points of the used assemblies. The implemented interface methods of the implementing class, are inside a try catch block that is catching all internal unhandled exceptions, creating a new Exception with a meaningfull description that is storing the internal exceptions as an inner exception.
