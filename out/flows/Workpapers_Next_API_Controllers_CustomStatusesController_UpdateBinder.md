[web] PUT /api/custom-statuses/binder/{id:Guid}  (Workpapers.Next.API.Controllers.CustomStatusesController.UpdateBinder)  [L127–L135] [auth=AuthorizationPolicies.Administrator]
  └─ writes_to BinderStatus [L131]
    └─ reads_from BinderStatuss
  └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
    └─ method WriteQuery [L131]

