[web] GET /api/accounting/reports/notes/policies/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.Get)  [L44–L48] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetPolicyQuery -> GetPolicyQueryHandler [L47]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetPolicyQueryHandler.Handle [L30–L68]
      └─ maps_to PolicyVariantForPolicyDto [L54]
        └─ automapper.registration CirrusMappingProfile (PolicyVariant->PolicyVariantForPolicyDto) [L811]
      └─ maps_to PolicyDto [L47]
        └─ automapper.registration CirrusMappingProfile (Policy->PolicyDto) [L789]
      └─ uses_service IControlledRepository<PolicyVariant> (Scoped (inferred))
        └─ method ReadQuery [L54]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.PolicyVariantRepository.ReadQuery
      └─ uses_service IControlledRepository<Policy> (Scoped (inferred))
        └─ method ReadQuery [L47]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.PolicyRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetPolicyQuery
    └─ handlers 1
      └─ GetPolicyQueryHandler
    └─ mappings 2
      └─ PolicyDto
      └─ PolicyVariantForPolicyDto

