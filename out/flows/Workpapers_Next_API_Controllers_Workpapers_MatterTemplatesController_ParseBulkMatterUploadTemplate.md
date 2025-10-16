[web] POST /api/matter-templates/create-checklist-models  (Workpapers.Next.API.Controllers.Workpapers.MatterTemplatesController.ParseBulkMatterUploadTemplate)  [L87–L109] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service StorageService
    └─ method GetFileBytes [L91]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.GetFileBytes [L17-L282]
  └─ uses_storage StorageService.GetFileBytes [L91]
  └─ impact_summary
    └─ services 1
      └─ StorageService
    └─ storages 1
      └─ StorageService

