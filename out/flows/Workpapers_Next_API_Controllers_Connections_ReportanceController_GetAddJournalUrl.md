[web] GET /api/connections/reportance/journal-url  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetAddJournalUrl)  [L84–L90] status=200
  └─ sends_request GetCirrusAddJournalUrl [L87]
    └─ handled_by Workpapers.Next.CirrusServices.Queries.GetCirrusJournalUrlHandler.Handle [L20–L34]
      └─ uses_service CirrusConfig
        └─ method SiteBaseUrl [L32]
          └─ ... (no implementation details available)

