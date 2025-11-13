Flow Comparison Notes (out6 vs new out)

Summary
- Compared 12 of the largest legacy flows from `D:\out6\flows` against the newly generated flows in `D:\out\flows` and `D:\out_new\flows`.
- Key gaps identified in new output prior to fixes:
  - Missing RequestProcessor narrative section: no `uses_service RequestProcessor`, `constructs RequestProcessorWrapper<,>`, `resolves IPipelineBehavior<,> chain`, `invokes IAsyncRequestHandler<,>.Handle`.
  - Missing `generic_pipeline_behaviors N` lines under dispatched requests.
  - The above items were conspicuous in out6 (e.g., controllers.all.md, FilesController.AddEntity.md) but absent in new flows.

Files Reviewed (largest 12 from out6)
- controllers.all.md
- Cirrus_Api_Controllers_Accounting_FilesController_AddEntity.md
- Workpapers_Next_API_Controllers_Connections_ClassController_GetFund.md
- Workpapers_Next_API_Controllers_Workpapers_WorkpaperRecordsController_BulkUpdate.md
- Cirrus_Api_Controllers_Accounting_Assets_AssetReportController_GetDetailReport.md
- Cirrus_Api_Controllers_Accounting_Reports_ReportTemplatesController_Create.md
- Cirrus_Api_Controllers_Accounting_Ledger_JournalController_Create.md
- Dataverse_Api_Controllers_UI_Documents_DocumentsController_BulkDeleteDocuments.md
- Cirrus_Api_Controllers_Accounting_DatasetsController_Create.md
- Workpapers_Next_API_Controllers_Connections_XeroController_GetAccountingFiles.md
- Workpapers_Next_API_Controllers_Connections_Bgl360Controller_GetFund.md
- Workpapers_Next_API_Controllers_Workpapers_BindersController_Activate.md

Observations and Actions
1) RequestProcessor narrative was missing in new output
   - Evidence (out6): Files like `*FilesController_AddEntity.md` show:
     - `- [uses_service RequestProcessor](...)`
     - `- implementation RequestProcessor.ProcessAsync [heuristic]`
     - `- constructs RequestProcessorWrapper<CanIAccessFileQuery,bool>`
     - `- resolves IPipelineBehavior<CanIAccessFileQuery,bool> chain`
     - `- invokes IAsyncRequestHandler<CanIAccessFileQuery,bool>.Handle`
   - New (pre-fix): Only `- [dispatches ...]` and `handled_by`, no RequestProcessor section.
   - Root cause: For controller-initiated dispatches, facts didn’t carry a RequestProcessor hint, and legacy renderer only triggered RequestProcessor narrative when `service` was present on the `sends_request` edge.
   - Fixes implemented:
     - LegacyNarrativeRenderer: treat edges with invocation `Process/ProcessAsync` as RequestProcessor invocations even if `service` is missing.
     - Controllers analyzer: when detecting IRequestProcessor.Process/ProcessAsync, also record a controller-level request fact with invocation name, so the renderer has context.

2) Pipeline behaviors not rendered for controller dispatches
   - Evidence (out6): `generic_pipeline_behaviors N` with behavior names was present.
   - New (pre-fix): Absent on controller-originated dispatches.
   - Root cause: `pipeline_behaviors` was never attached to controller `sends_request` facts.
   - Fix implemented: Attach `pipeline_behaviors` to controller `sends_request` facts via `ResolvePipelineBehaviorsForRequest` so the renderer can list them.

Verification
- Regenerated flows to `D:\out_new\flows`.
- Confirmed presence of RequestProcessor blocks and behavior lists (examples):
  - `D:\out_new\flows\Cirrus_Api_Controllers_Accounting_Assets_AssetController_POST_BulkCreate_api_accounting_assets_assets_bulk.md` now includes `uses_service RequestProcessor`, `constructs RequestProcessorWrapper<,>`, and `generic_pipeline_behaviors`.
  - `D:\out_new\flows\Cirrus_Api_Controllers_Accounting_FilesController_PUT_AddEntity_*.md` now includes the same.
  - grep checks: found thousands of `constructs RequestProcessorWrapper<` occurrences and many `generic_pipeline_behaviors` lines.

Remaining Items to watch
- Continue spot-checking legacy vs new for niche cases (e.g., unusual DI registration patterns, custom mediator wrappers) to ensure behavior lists resolve; if any remain, extend DI/pipeline scanning heuristics accordingly.
- Cross-solution HTTP flow linking: added token/wildcard route matching in ClientLinker and narrative fallback call synthesis when facts lack explicit calls edges. This restores nested `calls ... target_service ... [web]` sections for client calls that provide verb/route. Calls with unknown/empty routes remain as `uses_client` only (same as some legacy cases).

Conclusion
- The major gaps between out6 and new flows for request dispatch narrative and pipeline behavior listing are resolved. Further iteration can target any remaining niche patterns discovered during broader review.
