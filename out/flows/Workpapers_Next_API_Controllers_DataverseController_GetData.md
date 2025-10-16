[web] GET /api/dataverse/{entityRoute}/audit  (Workpapers.Next.API.Controllers.DataverseController.GetData)  [L367–L379] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L374]
      └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
  └─ sends_request DataverseAuditQuery [L376]
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
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L63]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L56]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Team>
        └─ method ReadQuery [L58]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L60]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L56]
          └─ ... (no implementation details available)

