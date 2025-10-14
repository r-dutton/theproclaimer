[web] GET /api/ui/account-mapping/mapping-codes  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.GetMappingCodes)  [L59–L68] [auth=Authentication.UserPolicy]
  └─ sends_request GetMappingCodesQuery [L67]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.MappingCodes.GetMappingCodesQueryHandler.Handle [L36–L107]
      └─ maps_to MappingCodeDto [L89]
      └─ maps_to MappingCodeDto [L76]
      └─ uses_service IControlledRepository<ExcludedMappingCode>
        └─ method ReadQuery [L65]
      └─ uses_service IControlledRepository<MappingCode>
        └─ method ReadQuery [L53]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L79]
      └─ uses_service UserService
        └─ method IsMaster [L58]

