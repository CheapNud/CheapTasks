# CheapTasks

Just another tasks/todo app. Public-facing Blazor Server, shipped as a Docker image, auto-deployed to Megaton via GHCR + WUD.

## Stack

- **.NET 11** + **Blazor Server** (InteractiveServer)
- **MudBlazor 9.2.0** UI
- **CheapHelpers 3.4.3** (Blazor / EF / Models)
- **EF Core 10** — SQLite for dev, PostgreSQL (VAULT-TEC) for prod
- **Serilog** → Seq (`192.168.1.15:5341`)
- **Docker** — single image on GHCR

## Repo layout

```
CheapTasks/
├── CheapTasks.slnx
├── CheapTasks.csproj         # project in root, no src/
├── Program.cs
├── appsettings*.json
├── Components/
│   ├── App.razor
│   ├── Routes.razor
│   ├── Layout/MainLayout.razor
│   └── Pages/Home.razor
├── wwwroot/
├── Dockerfile
├── docker-compose.yml        # Megaton runtime
├── .gitea/workflows/         # build + claude PR review (primary)
└── .github/workflows/        # mirror — CI + GHCR publish on tag
```

## Run locally

```bash
dotnet run --project CheapTasks.csproj
```

App listens on `http://localhost:5273` (dev SQLite, no auth wired yet).

## Deploy pipeline

1. Push to `main` (Gitea, primary) → CI builds + tests
2. Tag `v*.*.*` → GitHub mirror picks it up → `publish.yml` builds Docker image, pushes to **GHCR** (`ghcr.io/cheapnud/cheaptasks:latest` + tagged version)
3. **WUD on Megaton** detects the new image (label `wud.watch=true`) → pulls, recreates container, prunes old image

No SSH, no manual step. Same pattern as CheapNights / CheapManga.

## Hosting

- **Primary git**: Gitea (Sierra-Madre, `192.168.1.15:3000`)
- **Mirror**: GitHub (CheapNud)
- **Image registry**: GHCR (`ghcr.io/cheapnud/cheaptasks`)
- **Runtime**: Megaton VM (`/opt/blazor-apps/cheaptasks/`, port `5030:5000`)
- **Database**: VAULT-TEC PostgreSQL (`192.168.1.14:5432`, db `cheaptasks`)
