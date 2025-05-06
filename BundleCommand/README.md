# File Bundler Command

📦 This is the main CLI tool `bundle` that merges code files of a specific language into a single `.txt` file.

## Usage

```bash
dotnet run --project BundleCommand -- bundle [options]
```

## Available Options

| Option                   | Alias | Description |
|--------------------------|-------|-------------|
| `--output`               | `-o`  | Name of the output file (default: `bundle.txt`) |
| `--language`             | `-l`  | Programming language to bundle (required). Options: `python`, `java`, `html`, `jsx`, `c++`, `c#`, `c`, `css`, `js`, `sql`, `json`, `all` |
| `--author`               | `-a`  | Adds the author name as a comment in the file |
| `--note`                 | `-n`  | Adds the source folder path as a comment at the top |
| `--sort`                 | `-s`  | Sorting method: `abc` (default), `type` |
| `--remove-empty-lines`   | `-r`  | Removes empty lines from the merged output |

## Example

```bash
dotnet run --project BundleCommand -- bundle -l c# -a "Yossi" -n -o output.txt -s abc -r
```
 
