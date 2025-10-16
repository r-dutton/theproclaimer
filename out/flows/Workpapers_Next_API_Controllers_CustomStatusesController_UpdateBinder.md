[web] PUT /api/custom-statuses/binder/{id:Guid}  (Workpapers.Next.API.Controllers.CustomStatusesController.UpdateBinder)  [L131–L139] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ write BinderStatus [L135]
    └─ reads_from BinderStatuss
  └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
    └─ method WriteQuery [L135]
      └─ ... (no implementation details available)

