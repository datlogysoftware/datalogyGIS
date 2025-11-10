# Security Policy

## Supported Versions

We release security updates for the following versions:

| Version | Supported          |
| ------- | ------------------ |
| 3.0.x   | :white_check_mark: |
| < 3.0   | :x:                |

## Security Features

DatalogySoftware GIS Framework includes the following security features:

### Input Validation
- **Table Name Sanitization**: All table names are validated to prevent SQL injection attacks
- **Parameter Validation**: Null checks and type validation on all public APIs
- **Coordinate Validation**: Latitude and longitude bounds checking

### Data Protection
- **Parameterized Queries**: All database queries use parameterized statements
- **JSON Serialization**: Safe JSON serialization with error handling
- **Geometry Validation**: WKB/WKT geometry validation

### Error Handling
- **Comprehensive Logging**: Detailed error logging without exposing sensitive information
- **Exception Handling**: Proper exception handling with informative messages
- **Graceful Degradation**: Fallback mechanisms for data parsing errors

## Reporting a Vulnerability

We take security vulnerabilities seriously. If you discover a security issue, please follow these steps:

### 1. Do Not Open a Public Issue
Please do not create a public GitHub issue for security vulnerabilities.

### 2. Email Us Directly
Send details to: **security@datalogysoft.com**

Include:
- Description of the vulnerability
- Steps to reproduce
- Potential impact
- Suggested fix (if any)

### 3. Response Timeline
- **Initial Response**: Within 48 hours
- **Status Update**: Within 5 business days
- **Fix Timeline**: Depends on severity
  - Critical: 1-7 days
  - High: 7-30 days
  - Medium: 30-90 days
  - Low: Next regular release

### 4. Disclosure Policy
- We will acknowledge your contribution in the release notes (unless you prefer to remain anonymous)
- We follow coordinated disclosure practices
- We will notify you before publicly disclosing the vulnerability

## Security Best Practices

When using DatalogySoftware GIS Framework:

### Database Security
```csharp
// ✅ GOOD: Use valid table names
var repository = new SqliteFeatureRepository(connection, "features", logger);

// ❌ BAD: Don't use user input directly as table names
var repository = new SqliteFeatureRepository(connection, userInput, logger);
```

### Connection Strings
```csharp
// ✅ GOOD: Store connection strings securely
var connectionString = configuration.GetConnectionString("GisDatabase");

// ❌ BAD: Don't hardcode connection strings
var connectionString = "Server=localhost;Database=gis;Password=secret123";
```

### Attribute Validation
```csharp
// ✅ GOOD: Validate user input before storing
if (!string.IsNullOrWhiteSpace(userInput))
{
    feature.Attributes["user_note"] = SanitizeInput(userInput);
}

// ❌ BAD: Don't store unvalidated user input
feature.Attributes["user_note"] = userInput;
```

### Coordinate Validation
```csharp
// ✅ GOOD: Use the builder which validates coordinates
var feature = Feature.Builder()
    .WithPoint(longitude: lon, latitude: lat)
    .Build();

// The Coordinate constructor automatically validates:
// - Latitude: -90 to 90
// - Longitude: -180 to 180
```

## Security Updates

### Subscribing to Security Notifications
- Watch this repository on GitHub
- Subscribe to our security mailing list: security-announce@datalogysoft.com
- Follow us on Twitter: @DatalogySoft

### Release Security Notes
Security fixes are documented in:
- CHANGELOG.md
- GitHub Security Advisories
- Release notes

## Known Security Considerations

### SQL Injection Prevention
- **Status**: ✅ Protected
- **Implementation**: Table name validation with regex pattern matching
- **Pattern**: `^[a-zA-Z_][a-zA-Z0-9_]*$`

### JSON Deserialization
- **Status**: ✅ Protected
- **Implementation**: Try-catch blocks with logging and fallback to empty dictionaries
- **Risk**: Malformed JSON attributes will not crash the application

### Spatial Query Safety
- **Status**: ✅ Protected
- **Implementation**: Parameterized spatial queries
- **Note**: Spatialite functions (MbrIntersects, BuildMbr) are safe with parameterized inputs

## Compliance

DatalogySoftware GIS Framework follows:
- OWASP Top 10 security practices
- .NET Security Guidelines
- Industry-standard secure coding practices

## Dependencies

We regularly update dependencies to patch security vulnerabilities:
- Automated: Dependabot checks weekly
- Manual: Security team reviews critical updates daily

## Security Audits

- **Last Audit**: 2025-11-09
- **Next Audit**: 2025-12-09
- **Auditor**: Internal Security Team

## Contact

- **Security Team**: security@datalogysoft.com
- **General Support**: support@datalogysoft.com
- **Website**: https://www.datalogysoft.com/security

---

**Thank you for helping keep DatalogySoftware GIS Framework secure!**
