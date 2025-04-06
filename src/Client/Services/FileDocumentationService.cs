using System.Text;
using Microsoft.Extensions.FileProviders;

namespace Client.Services;

public interface IDocumentationService
{
    IEnumerable<DocSection> GetSections();
    Task<string?> GetMarkdownAsync(string section, string slug);
    string ToTitleCase(string input);
}

public class FileDocumentationService : IDocumentationService
{
    private readonly string _docsRoot;
    private readonly PhysicalFileProvider _fileProvider;
    private readonly Dictionary<string, Dictionary<string, string>> _index = new();

    public FileDocumentationService(IWebHostEnvironment env)
    {
        _docsRoot = Path.Combine(env.ContentRootPath, "docs");
        _fileProvider = new PhysicalFileProvider(_docsRoot);

        foreach (var sectionDir in Directory.EnumerateDirectories(_docsRoot))
        {
            var sectionKey = Path.GetFileName(sectionDir)!;
            var files = Directory.EnumerateFiles(sectionDir, "*.md");
            var sectionDocs = new Dictionary<string, string>();

            foreach (var file in files)
            {
                var slug = Path.GetFileNameWithoutExtension(file);
                var title = ToTitleCase(slug.Replace("-", " "));
                sectionDocs[title] = slug;
            }

            _index[sectionKey] = sectionDocs;
        }
    }

    public IEnumerable<DocSection> GetSections()
    {
        return _index.Select(kvp => new DocSection
        {
            Section = kvp.Key,
            Pages = kvp.Value.Select(p => new DocPage { Title = p.Key, Slug = p.Value }).ToList()
        });
    }

    public async Task<string?> GetMarkdownAsync(string section, string slug)
    {
        var path = Path.Combine(_docsRoot, section, $"{slug}.md");
        if (!File.Exists(path)) return null;
        return await File.ReadAllTextAsync(path, Encoding.UTF8);
    }

    public string ToTitleCase(string input)
    {
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
    }
}

public record DocSection
{
    public string Section { get; init; } = "";
    public List<DocPage> Pages { get; init; } = new();
}

public record DocPage
{
    public string Title { get; init; } = "";
    public string Slug { get; init; } = "";
}