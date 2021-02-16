using Data;
using Unit.Player;


namespace CharacterCustomizing
{
    public sealed class CustomizerCharacter
    {
        private CharacterData _characterData;

        public CustomizerCharacter(CharacterData characterData)
        {
            _characterData = characterData;
        }

        public void Customize(IPlayerView playerView, CharacterSettings settings)
        {
            playerView.CharAttributes.AgroDistance = settings.AgroDistance;
            playerView.CharAttributes.Speed = settings.PlayerMoveSpeed;
            playerView.CharAttributes.RotateSpeedPlayer = settings.RotateSpeedPlayer;

            var person = new PersonCharacter(playerView.Transform.gameObject, _characterData);
            person.CharacterRace = settings.CharacterRace;
            person.CharacterGender = settings.CharacterGender;
            person.Generate();

            var equipmentPoints = new EquipmentPoints(playerView.Transform.gameObject, _characterData);
            equipmentPoints.GenerateAllPoints();
            playerView.EquipmentPoints = equipmentPoints;

            var equipWeapon = new EquipmentWeapon(playerView, settings.Equipment);
            equipWeapon.GetWeapons();
            playerView.AnimatorParameters.WeaponType = equipWeapon.GetWeaponType();
        }
    }
}