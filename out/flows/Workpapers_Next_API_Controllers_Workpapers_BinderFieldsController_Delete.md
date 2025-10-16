[web] DELETE /api/binder-fields/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.Delete)  [L105–L114] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ calls BinderFieldRepository.Remove [L111]
  └─ calls BinderFieldRepository.WriteQuery [L109]
  └─ delete BinderField [L111]
    └─ reads_from BinderFields
  └─ write BinderField [L109]
    └─ reads_from BinderFields
  └─ uses_service IControlledRepository<BinderField>
    └─ method WriteQuery [L109]
      └─ ... (no implementation details available)

