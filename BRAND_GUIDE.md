# DatalogySoftware GIS Framework - Brand Guidelines

## Brand Identity

### Company Information

```yaml
Company:      DatalogySoftware
Website:      https://www.datalogysoft.com
Product:      DatalogySoftware GIS Framework
Short Name:   Datalogy.Gis
Established:  2024
```

### Mission Statement

> "Empowering developers to build next-generation geospatial intelligence applications with modern .NET technology."

### Taglines

**Primary Tagline:**
> "Powering Next-Generation Geospatial Intelligence"

**Alternative Taglines:**
- "Modern GIS for Modern .NET"
- "Enterprise Geospatial Framework"
- "Build Smarter Maps, Faster"
- "Geospatial Intelligence Made Simple"

---

## Visual Identity

### Logo Usage

The DatalogySoftware GIS Framework logo consists of:
- Company name: **DatalogySoftware**
- Product name: **GIS Framework**
- Optional tagline

**Text Format:**
```
DatalogySoftware
    GIS Framework
"Powering Next-Generation Geospatial Intelligence"
```

### Color Palette

#### Primary Colors

| Color | Hex Code | RGB | Usage |
|-------|----------|-----|-------|
| **Blue** | `#0066CC` | `0, 102, 204` | Primary brand color, links, CTAs |
| **Green** | `#00A86B` | `0, 168, 107` | Success states, growth indicators |
| **Orange** | `#FF6B35` | `255, 107, 53` | Accent, warnings, highlights |

#### Neutral Colors

| Color | Hex Code | RGB | Usage |
|-------|----------|-----|-------|
| **Navy** | `#2C3E50` | `44, 62, 80` | Headers, dark text |
| **Light Gray** | `#ECF0F1` | `236, 240, 241` | Backgrounds, dividers |
| **White** | `#FFFFFF` | `255, 255, 255` | Backgrounds, contrast |
| **Black** | `#000000` | `0, 0, 0` | Body text |

### Typography

**Code/Technical:**
- Monospace fonts: Consolas, Monaco, 'Courier New'
- Font size: 14px base

**Headers:**
- Sans-serif: Segoe UI, Roboto, Arial
- Bold weights for emphasis

**Body:**
- Sans-serif: Segoe UI, Roboto, Arial
- Regular weight, 16px base

---

## Naming Conventions

### Namespace Pattern

All namespaces follow the pattern: `Datalogy.Gis.*`

**Examples:**
```csharp
Datalogy.Gis.Core
Datalogy.Gis.Domain
Datalogy.Gis.Domain.Entities
Datalogy.Gis.Data.Sqlite
Datalogy.Gis.Data.Postgres
Datalogy.Gis.Rendering.SkiaSharp
Datalogy.Gis.Maui
Datalogy.Gis.Blazor
```

### Package Naming

All NuGet packages follow: `Datalogy.Gis.*`

**Examples:**
- `Datalogy.Gis.Core`
- `Datalogy.Gis.Data.Sqlite`
- `Datalogy.Gis.Rendering.SkiaSharp`

### File Naming

- C# files: PascalCase (e.g., `Feature.cs`, `MapRenderer.cs`)
- Configuration: kebab-case (e.g., `appsettings.json`, `launch.json`)
- Documentation: UPPERCASE or kebab-case (e.g., `README.md`, `getting-started.md`)

---

## Brand Voice

### Tone

- **Professional**: Enterprise-grade, reliable
- **Developer-Friendly**: Clear, concise, helpful
- **Modern**: Cutting-edge, innovative
- **Confident**: Authoritative without being arrogant

### Language Guidelines

**Do:**
- Use active voice
- Be direct and clear
- Focus on benefits
- Use technical terms appropriately
- Provide examples

**Don't:**
- Use jargon without explanation
- Make unsubstantiated claims
- Use excessive marketing speak
- Patronize developers

### Example Phrases

**Good:**
- "Build sophisticated mapping applications with ease"
- "Enterprise-grade performance meets developer-friendly APIs"
- "Seamlessly integrate geospatial data into your .NET applications"

**Avoid:**
- "The world's best GIS framework" (unsubstantiated)
- "Revolutionary paradigm shift" (marketing jargon)
- "Simply the easiest" (oversimplification)

---

## Brand Messaging

### Key Messages

1. **Enterprise-Ready**
   - Built for mission-critical applications
   - Scalable, reliable, secure
   - Production-proven technology stack

2. **Developer-First**
   - Intuitive, fluent API design
   - Comprehensive documentation
   - Rich examples and samples

3. **Cloud-Native**
   - Deploy anywhere: Azure, AWS, on-premises
   - Container-ready
   - Microservices-friendly

4. **High-Performance**
   - Optimized spatial indexing
   - Efficient rendering engine
   - Async/await throughout

5. **Modern Stack**
   - .NET 8 and C# 12
   - Cross-platform support
   - Latest language features

### Target Audiences

1. **Enterprise Developers**
   - Building mission-critical GIS applications
   - Need reliability, scalability, support

2. **Independent Developers**
   - Creating innovative mapping solutions
   - Want flexibility, good documentation

3. **Startups**
   - Building location-based services
   - Need rapid development, cloud-native

4. **.NET Shops**
   - Existing .NET infrastructure
   - Want to add geospatial capabilities

---

## Marketing Materials

### Elevator Pitch (30 seconds)

> "DatalogySoftware GIS Framework is a modern, high-performance geospatial platform for .NET developers. Built on .NET 8, it provides everything you need to build sophisticated mapping and spatial analysis applications—from SQLite to PostgreSQL, from desktop to mobile, from on-premises to cloud. With our developer-friendly API and comprehensive documentation, you can go from zero to production-ready maps in minutes."

### One-Liner

> "Modern GIS framework for .NET—build powerful geospatial applications with ease."

### Feature Highlights

When describing the framework, emphasize:
- **.NET 8 + C# 12**: Modern, cross-platform
- **Multiple Databases**: SQLite, PostgreSQL, Cosmos DB
- **Rich Rendering**: SkiaSharp-based visualization
- **UI Components**: Ready for MAUI and Blazor
- **Cloud-Ready**: Azure, AWS, on-prem
- **Developer-Friendly**: Fluent API, great docs

---

## Online Presence

### Domains

- **Primary**: `https://www.datalogysoft.com`
- **Documentation**: `https://docs.datalogysoft.com/gis`
- **NuGet**: `https://nuget.org/packages/Datalogy.Gis.Core`

### Social Media

- **Twitter/X**: `@datalogysoft`
- **GitHub**: `https://github.com/datalogysoft/gis-framework`
- **LinkedIn**: DatalogySoftware

### Email

- **General**: `info@datalogysoft.com`
- **Support**: `support@datalogysoft.com`
- **Sales**: `sales@datalogysoft.com`

---

## Package Metadata Template

All NuGet packages should include:

```xml
<PropertyGroup>
  <Company>DatalogySoftware</Company>
  <Authors>DatalogySoftware</Authors>
  <Product>DatalogySoftware GIS Framework</Product>
  <Copyright>Copyright © 2024 DatalogySoftware</Copyright>

  <PackageId>[Package Name]</PackageId>
  <Version>3.0.0-preview</Version>
  <Description>[Specific package description]</Description>

  <PackageProjectUrl>https://www.datalogysoft.com/gis</PackageProjectUrl>
  <PackageReadmeFile>README.md</PackageReadmeFile>
  <PackageLicenseExpression>MIT</PackageLicenseExpression>
  <RepositoryUrl>https://github.com/datalogysoft/gis-framework</RepositoryUrl>
  <RepositoryType>git</RepositoryType>
  <PackageTags>gis;geospatial;datalogysoft;datalogy;spatial;mapping</PackageTags>
  <PackageIcon>icon.png</PackageIcon>
</PropertyGroup>
```

---

## Documentation Standards

### README Structure

Every package README should include:
1. Package name and tagline
2. Badges (license, version, build status)
3. Quick description
4. Installation instructions
5. Quick start example
6. Key features
7. Links to full documentation
8. License information
9. Support contacts

### Code Examples

All code examples should:
- Be complete and runnable
- Include necessary `using` statements
- Follow C# coding conventions
- Include comments for complex logic
- Demonstrate real-world use cases

### API Documentation

- Use XML documentation comments
- Provide example usage in `<example>` tags
- Document exceptions with `<exception>` tags
- Include parameter descriptions
- Add `<remarks>` for important notes

---

## Brand Checklist

When creating new materials, ensure:

- [ ] Company name is "DatalogySoftware" (one word, capital D, capital S)
- [ ] Product name is "DatalogySoftware GIS Framework"
- [ ] All namespaces start with `Datalogy.Gis.*`
- [ ] All packages start with `Datalogy.Gis.*`
- [ ] Copyright is "Copyright © 2024 DatalogySoftware"
- [ ] Website is `https://www.datalogysoft.com`
- [ ] Repository is `https://github.com/datalogysoft/gis-framework`
- [ ] Email domain is `@datalogysoft.com`
- [ ] License is MIT
- [ ] Colors match brand palette
- [ ] Tone is professional and developer-friendly

---

## Version History

- **v3.0.0-preview** (2024): Initial brand guidelines
- Framework rebranded from previous identity to DatalogySoftware

---

<p align="center">
  <strong>DatalogySoftware GIS Framework</strong><br>
  Powering Next-Generation Geospatial Intelligence<br>
  <br>
  © 2024 DatalogySoftware. All rights reserved.
</p>
