[web] GET /api/dataverse/{entityRoute}/audit  (Workpapers.Next.API.Controllers.DataverseController.GetData)  [L366–L378] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L373]
  └─ sends_request DataverseAuditQuery [L375]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.DataverseAuditQueryHandler.Handle [L26–L69]
      └─ maps_to DataverseAuditDto [L63]
        └─ automapper.registration CirrusMappingProfile (Entity->DataverseAuditDto) [L178]
      └─ maps_to DataverseAuditDto [L62]
        └─ automapper.registration CirrusMappingProfile (Client->DataverseAuditDto) [L162]
        └─ automapper.registration DataverseMappingProfile (Client->DataverseAuditDto) [L81]
      └─ maps_to DataverseAuditDto [L60]
        └─ automapper.registration DataverseMappingProfile (User->DataverseAuditDto) [L78]
        └─ automapper.registration CirrusMappingProfile (User->DataverseAuditDto) [L123]
      └─ maps_to DataverseAuditDto [L58]
        └─ automapper.registration CirrusMappingProfile (Team->DataverseAuditDto) [L143]
      └─ maps_to DataverseAuditDto [L56]
        └─ automapper.registration CirrusMappingProfile (Office->DataverseAuditDto) [L135]
        └─ automapper.registration DataverseMappingProfile (Office->DataverseAuditDto) [L79]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L62]
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L63]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L56]
      └─ uses_service IControlledRepository<Team>
        └─ method ReadQuery [L58]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L60]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L56]

