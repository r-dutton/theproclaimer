[web] GET /api/internal/account-mapping/mapping-codes/{code}  (Dataverse.Api.Controllers.Internal.AccountMapping.MappingCodesController.GetMappingCodeByCode)  [L44–L48] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetMappingCodeQuery [L47]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.MappingCodes.GetMappingCodeQueryHandler.Handle [L38–L75]
      └─ maps_to MappingCodeDto [L68]
      └─ uses_service UserService
        └─ method IsMaster [L59]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<ExcludedMappingCode>
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<MappingCode>
        └─ method ReadQuery [L56]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L69]
          └─ ... (no implementation details available)

