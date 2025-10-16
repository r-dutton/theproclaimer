[web] PUT /api/accounting/reports/notes/disclosures/variants/{id:Guid}/confirmMasterChangesChecked  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.ConfirmMasterChangesChecked)  [L153–L162] status=200 [auth=Authentication.UserPolicy]
  └─ calls DisclosureVariantRepository.WriteQuery [L156]
  └─ write DisclosureVariant [L156]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method WriteQuery [L156]
      └─ ... (no implementation details available)

