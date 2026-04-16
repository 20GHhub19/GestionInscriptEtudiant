# Rapport LaTeX

Rapport formaté du projet `GestionInscriptEtudiant`.

## Compilation

### Option 1 — Overleaf (le plus simple)
Uploader `rapport.tex` et le dossier `figures/` sur [overleaf.com](https://overleaf.com) et compiler avec pdfLaTeX.

### Option 2 — TeX Live / MiKTeX local
```bash
cd report
pdflatex rapport.tex
pdflatex rapport.tex   # 2e passe pour la table des matières
```

## À personnaliser avant la remise

1. **Matricules** — remplacer les `XXXXXXXX` en haut de `rapport.tex` :
   ```latex
   \newcommand{\matEdouard}{20XXXXXX}
   \newcommand{\matFlorion}{20XXXXXX}
   \newcommand{\matGaius}{20XXXXXX}
   \newcommand{\matImad}{20XXXXXX}
   ```

2. **Captures d'écran** — placer les images PNG/JPG dans `figures/` et
   décommenter les lignes `\includegraphics` correspondantes :
   - `figures/schema_EA.png`
   - `figures/accueil.png`
   - `figures/dashboard_etudiant.png`
   - `figures/saisie_notes.png`
   - `figures/dashboard_admin.png`

3. **Numéro de projet** — à ajouter sur la page de garde si fourni.
