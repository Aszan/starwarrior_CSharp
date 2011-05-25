using System;
using Artemis;
using StarWarrior.Components;
namespace StarWarrior
{
	public class PlayerShipControlSystem : EntityProcessingSystem {
		private GameContainer container;
		private bool moveRight;
		private bool moveLeft;
		private bool shoot;
		private ComponentMapper transformMapper;
	
		public PlayerShipControlSystem(GameContainer container) : base(typeof(Transform), typeof(Player)) {
			this.container = container;
		}
	
		public override void Initialize() {
			transformMapper = new ComponentMapper(typeof(Transform), world.GetEntityManager());
			container.GetInput().AddKeyListener(this);
		}
	
		public override void Process(Entity e) {
			Transform transform = transformMapper.Get<Transform>(e);
	
			if (moveLeft) {
				transform.AddX(world.GetDelta() * -0.3f);
			}
			if (moveRight) {
				transform.AddX(world.GetDelta() * 0.3f);
			}
			
			if (shoot) {
				Entity missile = EntityFactory.CreateMissile(world);
				missile.GetComponent<Transform>(typeof(Transform)).SetLocation(transform.GetX(), transform.GetY() - 20);
				missile.GetComponent<Velocity>(typeof(Velocity)).SetVelocity(-0.5f);
				missile.GetComponent<Velocity>(typeof(Velocity)).SetAngle(90);
				missile.Refresh();
	
				shoot = false;
			}
		}
	
		public override void KeyPressed(int key, char c) {
			if (key == Input.KEY_A) {
				moveLeft = true;
				moveRight = false;
			} else if (key == Input.KEY_D) {
				moveRight = true;
				moveLeft = false;
			} else if (key == Input.KEY_SPACE) {
				shoot = true;
			}
		}
	
		public override void KeyReleased(int key, char c) {
			if (key == Input.KEY_A) {
				moveLeft = false;
			} else if (key == Input.KEY_D) {
				moveRight = false;
			} else if (key == Input.KEY_SPACE) {
				shoot = false;
			}
		}
	
		public override void InputEnded() {
		}
	
		public override void InputStarted() {
		}
	
		public override bool IsAcceptingInput() {
			return true;
		}
	
		public override void SetInput(Input input) {
		}
	
	}
}