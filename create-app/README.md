 # Response File Generator

🧪 This is a step-by-step interactive CLI tool that helps generate a `.rsp` response file for the bundler.

## How to Run

```bash
dotnet run --project ResponseFileGenerator
```

The tool will ask for:
- Output filename
- Author name (optional)
- Whether to include the source folder path
- Sorting preference
- Language to bundle
- Whether to remove empty lines

At the end, a file named:
```
responFile.rsp
```
will be created in the root folder.

## How to Use the Response File

Once the file is generated, you can run the bundler like this:

```bash
dotnet run --project BundleCommand @responFile.rsp
```

📌 Make sure both `BundleCommand` and `ResponseFileGenerator` projects are in the same solution or accessible to each other.
