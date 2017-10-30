using HtmlAgilityPack;

namespace dna.core.libs.Extension
{
    public static class HtmlExtension
    {
        public static string RemoveHtmlTag(this string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            string plainText = htmlDoc.DocumentNode.InnerText;
            return plainText;
        }
    }
}
