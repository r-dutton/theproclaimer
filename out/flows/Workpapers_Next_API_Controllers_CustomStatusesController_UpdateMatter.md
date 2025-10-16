[web] PUT /api/custom-statuses/matter/{id:Guid}  (Workpapers.Next.API.Controllers.CustomStatusesController.UpdateMatter)  [L121–L129] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service IControlledRepository<MatterStatus> (AddScoped)
    └─ method WriteQuery [L125]
      └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ IControlledRepository<MatterStatus>

