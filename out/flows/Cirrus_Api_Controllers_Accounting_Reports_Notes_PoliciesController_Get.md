[web] GET /api/accounting/reports/notes/policies/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.Get)  [L44–L48] [auth=Authentication.UserPolicy]
  └─ sends_request GetPolicyQuery [L47]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetPolicyQueryHandler.Handle [L30–L68]
      └─ maps_to PolicyDto [L47]
        └─ automapper.registration CirrusMappingProfile (Policy->PolicyDto) [L789]
      └─ maps_to PolicyVariantForPolicyDto [L54]
        └─ automapper.registration CirrusMappingProfile (PolicyVariant->PolicyVariantForPolicyDto) [L811]
      └─ uses_service IControlledRepository<Policy>
        └─ method ReadQuery [L47]
      └─ uses_service IControlledRepository<PolicyVariant>
        └─ method ReadQuery [L54]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L57]

