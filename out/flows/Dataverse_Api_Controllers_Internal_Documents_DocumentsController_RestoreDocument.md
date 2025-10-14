[web] PUT /api/internal/documents/{id:guid}/restore-document  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.RestoreDocument)  [L123–L138] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request RestoreDocumentCommand [L130]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.RestoreDocumentCommandHandler.Handle [L23–L46]
  └─ sends_request CanIAccessDocumentQuery [L129]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L66]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L68]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L58]
      └─ uses_service UserService
        └─ method GetUserAsync [L61]

