[web] PUT /api/classifications/parse-account-numbers/{system}  (Workpapers.Next.API.Controllers.Workpapers.ClassificationsController.ParseAccountNumbers)  [L28–L32] [auth=AuthorizationPolicies.User]
  └─ sends_request ParseClassificationFromNumberQuery [L31]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.ParseClassificationFromNumberQueryHandler.Handle [L27–L286]

