<!--
  TODO.md — CheapTasks project work tracker
  Last updated: 2026-04-17b

  RULES FOR AI AGENTS:
  - Update the "Last updated" date above whenever you modify this file
  - Items use checkbox format: - [ ] incomplete, - [x] complete
  - Never remove completed items — they serve as history. Move them to "## Done" when a category gets cluttered.
  - Each item gets ONE line. Details go in sub-bullets indented with 2 spaces.
  - Prefix each item with the date it was added: - [ ] (2026-03-17) Description
  - When completing, change to: - [x] (2026-03-17 → 2026-03-18) Description
  - Tag the SOURCE of each item at the end in brackets:
      [code-todo] = from // TODO comment in source code
      [plan] = from a plan document or planning session
      [bug] = from a bug encountered during dev/deploy
      [audit] = from a code audit or review
      [user] = explicitly requested by the user
  - For [code-todo] items, ALWAYS include file:line reference so devs can navigate directly
  - Categories: Blocking, Planned, Future, Done
  - New items go at the TOP of their category
  - Do not create separate TODO_*.md files — everything goes here
  - Keep it terse. If it needs more than 3 sub-bullets, link to a plan document.
  - Do NOT create, rename, or remove categories — the fixed set is: Blocking, Planned, Future, Done
  - When asked for planned work or TODO analysis, ALWAYS include Future items too — list them below Planned and note them as future work
-->

# TODO

## Blocking

_Nothing blocking._

## Planned

- [ ] (2026-04-17) Smoke-test locally — `dotnet run`, sign in with Plex, add/complete/delete tasks [plan]
- [ ] (2026-04-17) Provision `cheaptasks` Postgres user + db on VAULT-TEC [plan]
  - Connection: `Host=192.168.1.14;Port=5432;Database=cheaptasks;Username=cheaptasks;Password=<secret>`
- [ ] (2026-04-17) Megaton: drop `docker-compose.yml` to `/opt/blazor-apps/cheaptasks/`, set `PG_PASSWORD`, `PLEX_ADMIN_TOKEN`, `PLEX_CALLBACK_BASE_URL` in `.env`, push v0.1.0 tag [plan]
- [ ] (2026-04-17) Confirm `wud.watch=true` is picked up by WUD on Megaton (check http://192.168.1.12:3000) [plan]
- [ ] (2026-04-17) Phase 2 trigger: drop in `Register.razor` from `CheapHelpers.Blazor.Pages.Account` when ready to open password sign-up [plan]

## Future

- [ ] (2026-04-17) Tags / labels on tasks [plan]
- [ ] (2026-04-17) Due-date reminders / notifications [plan]
- [ ] (2026-04-17) Public share link for a single task or list (read-only) [plan]
- [ ] (2026-04-17) Mobile layout pass [plan]
- [ ] (2026-04-17) Health + metrics endpoints (gated by `X-Api-Key`, like CheapManga) [plan]
- [ ] (2026-04-17) Rate-limit login endpoint [plan]

## Done

- [x] (2026-04-17 → 2026-04-17) Scaffold Blazor Server skeleton (App, Routes, MainLayout, Home) [user]
- [x] (2026-04-17 → 2026-04-17) `.slnx` + `.csproj` in repo root, no `src/` folder [user]
- [x] (2026-04-17 → 2026-04-17) MudBlazor 9.2.0 wired in `Program.cs` [user]
- [x] (2026-04-17 → 2026-04-17) Reference `CheapHelpers.{Blazor,EF,Models}` 3.4.3 (sibling parity) [user]
- [x] (2026-04-17 → 2026-04-17) Serilog → Seq config (`192.168.1.15:5341`) [user]
- [x] (2026-04-17 → 2026-04-17) `Dockerfile` + `docker-compose.yml` (GHCR image, port 5030, `wud.watch`) [user]
- [x] (2026-04-17 → 2026-04-17) Gitea workflows: `build.yml`, `pr-review.yml` [user]
- [x] (2026-04-17 → 2026-04-17) GitHub workflows: `dotnet.yml`, `publish.yml` (GHCR publish on tag) [user]
- [x] (2026-04-17 → 2026-04-17) `AppUser : CheapUser` + `TaskItem` model (Id, OwnerId, Title, Notes, Done, Created/Due/CompletedUtc) [plan]
- [x] (2026-04-17 → 2026-04-17) `CheapTasksDbContext : CheapCommunicationContext<AppUser>` (Identity + comms tables + Tasks) [plan]
- [x] (2026-04-17 → 2026-04-17) `TaskRepo` with `IDbContextFactory` pattern, owner-scoped CRUD [plan]
- [x] (2026-04-17 → 2026-04-17) Initial EF migration `Init` (Identity tables + UserNotificationPreferences + Tasks) [plan]
- [x] (2026-04-17 → 2026-04-17) Sqlite dev / Postgres prod wiring in `Program.cs` [plan]
- [x] (2026-04-17 → 2026-04-17) `AddIdentity<AppUser, IdentityRole>()` + `AddPlexAuth()` + `AddExternalUserProvisioning<AppUser>()` [plan]
- [x] (2026-04-17 → 2026-04-17) Plex sign-in locked to server members via `HasServerAccessAsync` (mirrors CheapNights) [plan]
- [x] (2026-04-17 → 2026-04-17) Rate limiter on `auth` endpoint (10/min per IP) [plan]
- [x] (2026-04-17 → 2026-04-17) Login.razor (Plex button, EmptyLayout) + Tasks.razor (`[Authorize]`, add/complete/delete) [plan]
- [x] (2026-04-17 → 2026-04-17) `AuthorizeRouteView` + `RedirectToLogin` for anonymous-redirect [plan]
