[web] GET /api/accounting/reports/notes/policies/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.Get)  [L44–L48] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetPolicyQuery [L47]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetPolicyQueryHandler.Handle [L30–L68]
      └─ maps_to PolicyDto [L47]
        └─ automapper.registration CirrusMappingProfile (Policy->PolicyDto) [L789]
      └─ maps_to PolicyVariantForPolicyDto [L54]
        └─ automapper.registration CirrusMappingProfile (PolicyVariant->PolicyVariantForPolicyDto) [L811]
      └─ uses_service IControlledRepository<Policy>
        └─ method ReadQuery [L47]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<PolicyVariant>
        └─ method ReadQuery [L54]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L57]
          └─ ... (no implementation details available)

