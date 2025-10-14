[web] POST /api/ui/account-mapping/mapping-codes/for-client-group/{clientGroupId:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.CreateMappingCode)  [L92–L100] [auth=Authentication.UserPolicy,Authentication.UserPolicy]
  └─ sends_request CreateMappingCodeCommand [L97]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.MappingCodes.CreateMappingCodeCommandHandler.Handle [L41–L75]
      └─ maps_to MappingCodeDto [L73]
      └─ uses_service IControlledRepository<MappingCode>
        └─ method ReadQuery [L60]
      └─ uses_service IMapper
        └─ method Map [L73]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L69]
      └─ uses_service UserService
        └─ method IsMaster [L58]

