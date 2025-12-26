using HtmlAgilityPack;

namespace Hamkare.Utility.Extensions;

public static class HtmlExtension
{
    public static int GetTimeReadOfHtml(this string htmlContent, int wordsPerMinute = 200)
    {
        if (string.IsNullOrWhiteSpace(htmlContent))
            return 0;

        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(htmlContent);

        var wordCount = htmlDoc.DocumentNode.DescendantsAndSelf()
            .Where(n => n.NodeType == HtmlNodeType.Text)
            .Select(textNode => textNode.InnerHtml.Split(new[]
            {
                ' ', '\t', '\n', '\r'
            }, StringSplitOptions.RemoveEmptyEntries))
            .Select(words => words.Length)
            .Sum();

        return wordCount / wordsPerMinute;
    }
}