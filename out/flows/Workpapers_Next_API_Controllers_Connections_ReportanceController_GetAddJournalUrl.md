[web] GET /api/connections/reportance/journal-url  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetAddJournalUrl)  [L84–L90]
  └─ sends_request GetCirrusAddJournalUrl [L87]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.CirrusServices.Queries.GetCirrusJournalUrlHandler.Handle [L20–L34]
      └─ uses_service CirrusConfig
        └─ method SiteBaseUrl [L32]

