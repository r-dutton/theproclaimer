[web] GET /api/connections/reportance/source/{datasetId}  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetSource)  [L60–L66] status=200
  └─ sends_request GetSourceQuery -> GetSourceQueryHandler [L63]
    └─ handled_by Workpapers.Next.CirrusServices.Queries.GetSourceQueryHandler.Handle [L20–L43]
      └─ uses_service CirrusHttp
        └─ method GetHttpResponseAsync [L39]
          └─ ... (no implementation details available)
      └─ uses_service CirrusConfig
        └─ method GetBaseUrl [L38]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ GetSourceQuery
    └─ handlers 1
      └─ GetSourceQueryHandler

