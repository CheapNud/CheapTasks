<!--
  TODO.md — CheapTasks project work tracker
  Last updated: 2026-05-01a

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

- [ ] (2026-05-01) Pin/star priority field on tasks — surface the few items actually being worked on above the backlog [plan]
  - Live data: 29/30 active, 1 done in 12 days; no current signal for "what's next"
- [ ] (2026-05-01) Rooms/Locations field (chip + filter) with autocomplete from existing values [plan]
  - Live data: heavy clustering on "boven", "berging", "bureau", "stoppenkast", "hal", "dakgoot"
  - User already disambiguated by hand: "Berging opruimen" (Apr 18) → "Berging boven opruimen" (Apr 22)
- [ ] (2026-05-01) Decision/research item type for `?`-style titles — question + options list + "decided" close-state [plan]
  - Live data: 5/30 titles end in "?" with embedded options (S/PDIF, RTX3070, HDMI fiber, multimeter, side spots)
- [ ] (2026-04-18) End-to-end sign-in test against live deployment (Plex pin → callback → land on /tasks) [user]
- [ ] (2026-04-18) Confirm `wud.watch=true` registered in WUD on Megaton (http://192.168.1.12:3000) [plan]
- [ ] (2026-04-18) Phase 2 (Identity bolt-on) blocked on CheapHelpers fix — make `ExternalUserProvisioner` generic over `TUser : CheapUser` and pull `UserManager<TUser>` / `SignInManager<TUser>` instead of the abstract base [plan]
  - Once 3.4.4 ships, swap `AddIdentity<AppUser, IdentityRole>()` + `AddExternalUserProvisioning<AppUser>()` back into Program.cs

## Future

- [ ] (2026-05-01) Compound-title splitter — detect `+` and "of op zn minst" → offer one-click sub-task split [plan]
  - Live examples: "Licht boven fixen + ventilator", "Kut vogel nest weg zien te krijgen + strips zetten", "die 2 side spots ophangen of op zn minst de draad clippen"
- [ ] (2026-05-01) Linked / follow-up parent-child relation between tasks [plan]
  - Live example: post-"Beamer ophangen" AV chain (S/PDIF, HDMI fiber, side spots) currently floating as siblings
- [ ] (2026-05-01) Auto-suggest moving parenthetical content from Title → Notes when title > ~60 chars or contains `(` [plan]
  - Live data: 0/30 use Notes; titles up to 113 chars with embedded prose ("Voltiq work (build that edge device, everything needed has arrived)")
- [ ] (2026-05-01) Stale chip + weekly "still relevant?" sweep for items > 14 days old [plan]
  - Live data: 17/30 active items would qualify today
- [ ] (2026-04-17) Tags / labels on tasks [plan]
  - Likely superseded by the Rooms/Locations item above; reconsider once Locations ships
- [ ] (2026-04-17) Due-date reminders / notifications [plan]
  - Live data argues against: 0/30 prod items have a due date set — defer until field shows organic adoption
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
- [x] (2026-04-18 → 2026-04-18) Pivot: drop Identity from Program.cs (CheapHelpers `ExternalUserProvisioner` hard-typed to `UserManager<CheapUser>` — incompatible with `AddIdentity<AppUser>`). Mirror CheapNights bare-cookie pattern. [bug]
- [x] (2026-04-18 → 2026-04-18) Provision `cheaptasks` PG user + db on VAULT-TEC (`cheaptasks-vault-boy-2026`) [plan]
- [x] (2026-04-18 → 2026-04-18) Drop `docker-compose.yml` + `.env` to Megaton `/opt/blazor-apps/cheaptasks/` [plan]
- [x] (2026-04-18 → 2026-04-18) Tag `v0.1.0` and `v0.1.1` (port 5030 conflict with cheapclerk → bumped to 5040; SQLite snapshot vs Postgres pending changes → regen migration against Postgres) [plan]
- [x] (2026-04-18 → 2026-04-18) Live at `http://192.168.1.12:5040` — `/login` 200, `/tasks` 302, `Tasks` table + `__EFMigrationsHistory` row in Postgres [plan]
- [x] (2026-04-18 → 2026-04-18) Append CheapTasks section to tranquility `CREDENTIALS.local.md` [plan]
- [x] (2026-04-18 → 2026-04-18) Fix Plex auth href paths in Login/MainLayout (`/auth/plex-start`, `/auth/logout` — defaults use hyphens) [bug]
- [x] (2026-04-18 → 2026-04-18) Add `UseForwardedHeaders` for reverse proxy (`tasks.cheapludes.be` via NPM) — Plex callback now resolves to public HTTPS URL, cookie `Secure` flag set correctly [user]
