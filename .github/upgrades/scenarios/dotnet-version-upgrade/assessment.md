# Assessment: .NET 10 Upgrade

Date: 2026-05-07

Solution: D:\repos\WZE-SymmetryPlanner\WZE-Symmetry-Planner.sln

Summary
-------
- Target chosen: .NET 10.0 (LTS)
- Assessment performed: package restore, outdated/vulnerable package scan, and a dry-build attempt for `net10.0`.

Key findings
------------
1. Restore failure due to package downgrade conflict involving AutoMapper:
   - A project references AutoMapper 16.0.0 while another dependency requires AutoMapper >= 16.1.1.
   - The restore fails with NU1605 (WarningAsError) and prevents a successful build for `net10.0`.
   - The Domain project and solution produced NU1903 warnings that AutoMapper 16.0.0 has a known high-severity vulnerability (see advisory linked by NuGet).

2. Build attempt for `net10.0` could not complete because restore failed (see above). Other projects restored successfully.

Detected projects and TFMs (high-level)
--------------------------------------
- Projects in solution include multiple projects; workspace context shows projects targeting .NET 8 and .NET 10.
- At least one project already targets net10.0; others may target net8.0 and will need TFM updates where desired.

Recommendations / Next steps
--------------------------
1. Fix the AutoMapper version conflict and vulnerability:
   - Add (or update) an explicit PackageReference to AutoMapper at a version that satisfies all consumers (recommend: 16.1.1 or later if available and patched) in the project(s) that currently reference 16.0.0.
   - Re-run `dotnet restore` and confirm no NU1605 errors remain.

2. Re-run the build attempt for `net10.0` after resolving package restore issues and capture any remaining API/compile errors.

3. Plan phase should include:
   - Package updates (list of packages flagged as outdated or vulnerable)
   - Per-project TFM updates (which projects to move to net10.0 vs keep at net8.0)
   - A small set of test builds to validate runtime compatibility

Assessment output and logs have been recorded to `.github/upgrades/scenarios/dotnet-version-upgrade/assessment-output.txt`.

If you want, I'll generate a plan that: (a) updates AutoMapper to a compatible/patched version, (b) updates project TFMs to net10.0 where appropriate, and (c) sequences package updates and build validation.
