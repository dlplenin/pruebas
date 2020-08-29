using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace pruebas.Models
{
    public class DemoFontResolver : IFontResolver
    {
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // Ignore case of font names.
            var name = familyName.ToLower();

            // Deal with the fonts we know.
            switch (name)
            {
                case "drift":
                    return new FontResolverInfo("Drift Wood");
            }

            // We pass all other font requests to the default handler.
            // When running on a web server without sufficient permission, you can return a default font at this stage.
            return PlatformFontResolver.ResolveTypeface(familyName, isBold, isItalic);
        }

        /// <summary>
        /// Return the font data for the fonts.
        /// </summary>
        public byte[] GetFont(string faceName)
        {
            switch (faceName)
            {
                case "Drift Wood":
                    return DemoFontHelper.Drift;
            }

            //return GetFont(faceName);
            return null;
        }
    }
}

/// <summary>
/// Helper class that reads font data from embedded resources.
/// </summary>
public static class DemoFontHelper
{
    public static byte[] Drift
    {
        get { return LoadFontData("pruebas.Drift.ttf"); }
    }

    /// <summary>
    /// Returns the specified font from an embedded resource.
    /// </summary>
    static byte[] LoadFontData(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using Stream stream = assembly.GetManifestResourceStream(name);
        if (stream == null)
            throw new ArgumentException("No resource with name " + name);

        int count = (int)stream.Length;
        byte[] data = new byte[count];
        stream.Read(data, 0, count);
        return data;
    }
}
