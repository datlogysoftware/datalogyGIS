# GitHub Packages Setup Guide

## 🔗 NuGet Paketlerini GitHub'a Bağlama

DatalogySoftware GIS Framework paketleri hem **NuGet.org** hem de **GitHub Packages** üzerinden yayınlanmaktadır.

---

## 📦 Otomatik Yayınlama (GitHub Actions)

### Mevcut Yapılandırma

`.github/workflows/nuget-publish.yml` workflow'u bir tag push edildiğinde otomatik olarak:

1. ✅ **NuGet.org**'a paketleri yayınlar
2. ✅ **GitHub Packages**'a paketleri yayınlar
3. ✅ GitHub Release oluşturur

### Gerekli Secrets

| Secret | Açıklama | Nereden Alınır |
|--------|----------|----------------|
| `NUGET_API_KEY` | NuGet.org API key | https://www.nuget.org/account/apikeys |
| `GITHUB_TOKEN` | GitHub token | Otomatik sağlanır (Actions tarafından) |

---

## 🔑 NuGet.org API Key Oluşturma

### Adımlar:

1. **NuGet.org'a Giriş Yapın**
   - https://www.nuget.org/account/apikeys

2. **Create Butonuna Tıklayın**

3. **Ayarları Yapın:**
   - **Key Name**: `DatalogySoftware-GIS-Framework-Publish`
   - **Scopes**:
     - ✅ Push (seçili olmalı)
     - ✅ Push new packages and package versions
   - **Glob Pattern**: `Datalogy.Gis.*`
   - **Expiration**: 365 days (1 yıl)

4. **Generate Butonuna Tıklayın**

5. **API Key'i Kopyalayın** (bir daha gösterilmeyecek!)

### GitHub'a Ekleme:

1. Repository'ye gidin: https://github.com/datlogysoftware/datalogyGIS
2. **Settings** → **Secrets and variables** → **Actions**
3. **New repository secret** butonuna tıklayın
4. **Name**: `NUGET_API_KEY`
5. **Secret**: Kopyaladığınız API key'i yapıştırın
6. **Add secret** butonuna tıklayın

---

## 📥 Paket Kullanımı

### Seçenek 1: NuGet.org'dan (Önerilen)

```bash
dotnet add package Datalogy.Gis.Core
```

### Seçenek 2: GitHub Packages'dan

#### A. Komut Satırı ile:

```bash
# GitHub Packages source'u ekle
dotnet nuget add source https://nuget.pkg.github.com/datlogysoftware/index.json \
  --name github \
  --username YOUR_GITHUB_USERNAME \
  --password YOUR_GITHUB_PAT \
  --store-password-in-clear-text

# Paketi yükle
dotnet add package Datalogy.Gis.Core --source github
```

#### B. nuget.config ile:

Projenizin root klasörüne `nuget.config` dosyası oluşturun:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <!-- NuGet.org (varsayılan) -->
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />

    <!-- GitHub Packages -->
    <add key="github" value="https://nuget.pkg.github.com/datlogysoftware/index.json" />
  </packageSources>

  <packageSourceCredentials>
    <github>
      <add key="Username" value="YOUR_GITHUB_USERNAME" />
      <add key="ClearTextPassword" value="YOUR_GITHUB_PAT" />
    </github>
  </packageSourceCredentials>
</configuration>
```

**Not:** `YOUR_GITHUB_USERNAME` ve `YOUR_GITHUB_PAT` değerlerini kendi bilgilerinizle değiştirin.

#### C. GitHub Personal Access Token (PAT) Oluşturma:

1. https://github.com/settings/tokens/new adresine gidin
2. **Note**: "NuGet Package Access"
3. **Expiration**: 90 days veya istediğiniz süre
4. **Scopes**:
   - ✅ `read:packages` - Paket okuma yetkisi
5. **Generate token** butonuna tıklayın
6. Token'i kopyalayın ve güvenli bir yerde saklayın

---

## 🚀 Yeni Release Yayınlama

### Manuel Yayınlama:

```bash
# Main branch'e geç
git checkout main
git pull origin main

# Tag oluştur (örnek: v3.0.1)
git tag -a v3.0.1 -m "Release v3.0.1 - Bug fixes and improvements"

# Tag'i push et
git push origin v3.0.1
```

GitHub Actions otomatik olarak:
1. Tüm paketleri derler
2. NuGet.org'a yayınlar
3. GitHub Packages'a yayınlar
4. GitHub Release oluşturur

### Workflow Durumunu Kontrol:

```bash
# Workflow'u izle
gh run list --workflow=nuget-publish.yml --limit 5

# Belirli bir run'ı izle
gh run watch RUN_ID
```

---

## 🔍 Yayınlanan Paketleri Kontrol Etme

### NuGet.org:
- https://www.nuget.org/packages/Datalogy.Gis.Core
- https://www.nuget.org/packages/Datalogy.Gis.Domain
- vs.

### GitHub Packages:
- https://github.com/orgs/datlogysoftware/packages?repo_name=datalogyGIS

### GitHub Releases:
- https://github.com/datlogysoftware/datalogyGIS/releases

---

## ⚠️ Sorun Giderme

### "401 Unauthorized" Hatası (NuGet.org)

**Neden:** `NUGET_API_KEY` eksik veya hatalı

**Çözüm:**
1. GitHub repo → Settings → Secrets → Actions
2. `NUGET_API_KEY` kontrolü yap
3. Gerekirse yeni API key oluştur ve güncelle

### "401 Unauthorized" Hatası (GitHub Packages)

**Neden:** `GITHUB_TOKEN` izinleri yetersiz

**Çözüm:**
1. Repository Settings → Actions → General
2. Workflow permissions → "Read and write permissions" seç
3. "Allow GitHub Actions to create and approve pull requests" işaretle
4. Save

### Paket Bulunamıyor (GitHub Packages)

**Neden:** Repository private olabilir veya PAT yetersiz

**Çözüm:**
1. Repository'nin public olduğundan emin olun
2. PAT'ın `read:packages` iznine sahip olduğunu kontrol edin
3. nuget.config'de username ve PAT'ın doğru olduğundan emin olun

---

## 📊 Mevcut Durum

| Platform | Status | URL |
|----------|--------|-----|
| **NuGet.org** | ⏳ Beklemede | https://www.nuget.org/profiles/DatalogySoftware |
| **GitHub Packages** | ✅ Hazır | https://github.com/orgs/datlogysoftware/packages |
| **GitHub Releases** | ✅ v3.0.0-preview | https://github.com/datlogysoftware/datalogyGIS/releases |

**Not:** NuGet.org yayını için `NUGET_API_KEY` secret'ının eklenmesi bekleniyor.

---

## 📞 Destek

Sorularınız için:
- **Issues**: https://github.com/datlogysoftware/datalogyGIS/issues
- **Email**: support@datalogysoft.com
- **Website**: https://www.datalogysoft.com

---

**Son Güncelleme:** 2025-11-09
**Framework Version:** v3.0.0-preview
