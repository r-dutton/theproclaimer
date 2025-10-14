[web] PUT /api/binder-roll-over/validate  (Workpapers.Next.API.Controllers.Workpapers.BinderRollOverController.ValidateRolloverFile)  [L91–L115] [auth=AuthorizationPolicies.User]
  └─ sends_request GetRollOverFileCommand [L94]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Binders.GetRollOverFileCommandHandler.Handle [L27–L67]
      └─ uses_service IHttpClientFactory
        └─ method CreateClient [L65]
      └─ uses_service StorageService
        └─ method GetFileBytes [L56]
  └─ sends_request ValidateRollOverFileCommand [L112]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Binders.ValidateRollOverFileCommandHandler.Handle [L26–L64]

