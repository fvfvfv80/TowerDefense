namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public interface ITowerModuleHost : ITowerWeaponHost
    {
        public TowerBaseModule TowerBaseModule { get; }
        public TowerWeaponModule TowerWeaponModule { get; }
    }
}