[web] PUT /api/accounting/reports/notes/policies/variants/{id:Guid}/confirmMasterChangesChecked  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.ConfirmMasterChangesChecked)  [L135–L144] [auth=Authentication.UserPolicy]
  └─ calls PolicyVariantRepository.WriteQuery [L138]
  └─ writes_to PolicyVariant [L138]
    └─ reads_from PolicyVariants
  └─ uses_service IControlledRepository<PolicyVariant>
    └─ method WriteQuery [L138]

