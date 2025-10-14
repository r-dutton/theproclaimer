[web] GET /api/internal/account-mapping/mapping-codes/{code}  (Dataverse.Api.Controllers.Internal.AccountMapping.MappingCodesController.GetMappingCodeByCode)  [L44–L48] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetMappingCodeQuery [L47]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.MappingCodes.GetMappingCodeQueryHandler.Handle [L38–L75]
      └─ maps_to MappingCodeDto [L68]
      └─ uses_service IControlledRepository<ExcludedMappingCode>
        └─ method ReadQuery [L62]
      └─ uses_service IControlledRepository<MappingCode>
        └─ method ReadQuery [L56]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L69]
      └─ uses_service UserService
        └─ method IsMaster [L59]

