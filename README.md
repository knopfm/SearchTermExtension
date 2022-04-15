# SearchTermExtensions
SearchTermExtensions is a library of IEnumerable extension methods to perform searching. The search feature provide parsing annoteded key terms.

## Usage
Include the library with a git submodule. A NuGet package is planed.

```C#
  var term = SearchTerm.Parse("term1");
  var results = source.Search(x => x.Property1, x => x.Property2).ApplyTerm(term);
```

## Support
> If you have any new feature requests, questions, or comments, please get in touch and raise an issue in this github repository.
