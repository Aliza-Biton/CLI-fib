# File Bundler CLI

🔧 A C# CLI project that bundles code files from different programming languages into a single text file, with options for filtering, sorting, annotations, and formatting.

## Folder Structure

- `BundleCommand` – Contains the code that performs the actual bundling based on command-line parameters.
- `ResponseFileGenerator` – An interactive utility that generates a `.rsp` response file (no need to type all CLI parameters manually).

## How to Use

1. You can run the bundler directly with command-line options (see `README` inside `BundleCommand`).
2. Or you can first run the response file generator (see `README` inside `ResponseFileGenerator`), then use the generated file to run the bundler:

```bash
dotnet run --project BundleCommand @responFile.rsp
```
## Run the CLI Globally (Optional)

If you want to run the `bundle` tool from any location in your terminal:

1. Build the project in Release mode:
   ```bash
   dotnet publish -c Release -o publish
   ```

2. Copy the compiled `.exe` (on Windows) or binary (on Linux/macOS) from the `publish` folder.

3. Add the path of the folder containing the executable to your system's **environment variables**.

4. Now you can run:
   ```bash
   bundle [options]
   ```
   from anywhere on your system!
