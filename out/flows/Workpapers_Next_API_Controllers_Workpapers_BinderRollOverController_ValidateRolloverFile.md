[web] PUT /api/binder-roll-over/validate  (Workpapers.Next.API.Controllers.Workpapers.BinderRollOverController.ValidateRolloverFile)  [L91–L115] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetRollOverFileCommand [L94]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Binders.GetRollOverFileCommandHandler.Handle [L27–L67]
      └─ uses_service StorageService
        └─ method GetFileBytes [L56]
          └─ implementation Workpapers.Next.Data.Storage.StorageService.GetFileBytes [L17-L282]
      └─ uses_service IHttpClientFactory
        └─ method CreateClient [L65]
          └─ ... (no implementation details available)
      └─ uses_storage StorageService.GetFileBytes [L56]
  └─ sends_request ValidateRollOverFileCommand [L112]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Binders.ValidateRollOverFileCommandHandler.Handle [L26–L64]

