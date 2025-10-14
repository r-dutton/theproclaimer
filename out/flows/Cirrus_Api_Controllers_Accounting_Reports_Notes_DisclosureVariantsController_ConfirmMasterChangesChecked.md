[web] PUT /api/accounting/reports/notes/disclosures/variants/{id:Guid}/confirmMasterChangesChecked  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.ConfirmMasterChangesChecked)  [L153–L162] [auth=Authentication.UserPolicy]
  └─ calls DisclosureVariantRepository.WriteQuery [L156]
  └─ writes_to DisclosureVariant [L156]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method WriteQuery [L156]

