var runSpeedScale = 1.0;
var walkSpeedScale = 1.0;
var torso : Transform;

function Awake ()
{
	// By default loop all animations
	GetComponent.<Animation>().wrapMode = WrapMode.Loop;

	// We are in full control here - don't let any other animations play when we start
	GetComponent.<Animation>().Stop();
	GetComponent.<Animation>().Play("idle");
}

function Update ()
{
	var marioController : ThirdPersonController = GetComponent(ThirdPersonController);
	var currentSpeed = marioController.GetSpeed();

	// Fade in run
	if (currentSpeed > marioController.walkSpeed)
	{
		GetComponent.<Animation>().CrossFade("run");
		// We fade out jumpland quick otherwise we get sliding feet
		GetComponent.<Animation>().Blend("jumpland", 0);
		//SendMessage("SyncAnimation", "run");
	}
	// Fade in walk
	else if (currentSpeed > 0.1)
	{
		GetComponent.<Animation>().CrossFade("walk");
		// We fade out jumpland realy quick otherwise we get sliding feet
		GetComponent.<Animation>().Blend("jumpland", 0);
		//SendMessage("SyncAnimation", "walk");
	}
	// Fade out walk and run
	else
	{
		GetComponent.<Animation>().CrossFade("idle");
		//SendMessage("SyncAnimation", "idle");
	}
	
	GetComponent.<Animation>()["run"].normalizedSpeed = runSpeedScale;
	GetComponent.<Animation>()["walk"].normalizedSpeed = walkSpeedScale;
	
	if (marioController.IsJumping ())
	{
		if (marioController.IsCapeFlying())
		{
			GetComponent.<Animation>().CrossFade ("jetpackjump", 0.2);
			//SendMessage("SyncAnimation", "jetpackjump");
		}
		else if (marioController.HasJumpReachedApex ())
		{
			GetComponent.<Animation>().CrossFade ("jumpfall", 0.2);
			//SendMessage("SyncAnimation", "jumpfall");
		}
		else
		{
			GetComponent.<Animation>().CrossFade ("jump", 0.2);
			//SendMessage("SyncAnimation", "jump");
		}
	}
	// We fell down somewhere
	else if (!marioController.IsGroundedWithTimeout ())
	{
		GetComponent.<Animation>().CrossFade ("ledgefall", 0.2);
		//SendMessage("SyncAnimation", "ledgefall");
	}
	// We are not falling down anymore
	else
	{
		GetComponent.<Animation>().Blend ("ledgefall", 0.0, 0.2);
	}
}

function DidLand () {
	GetComponent.<Animation>().Play("jumpland");
	//SendMessage("SyncAnimation", "jumpland");
}

function DidPunch () {
	GetComponent.<Animation>().CrossFadeQueued("punch", 0.3, QueueMode.PlayNow);
}

function DidButtStomp () {
	GetComponent.<Animation>().CrossFade("buttstomp", 0.1);
	//SendMessage("SyncAnimation", "buttstomp");
	GetComponent.<Animation>().CrossFadeQueued("jumpland", 0.2);
}

function ApplyDamage () {
	GetComponent.<Animation>().CrossFade("gothit", 0.1);
	//SendMessage("SyncAnimation", "gothit");
}


function DidWallJump ()
{
	// Wall jump animation is played without fade.
	// We are turning the character controller 180 degrees around when doing a wall jump so the animation accounts for that.
	// But we really have to make sure that the animation is in full control so 
	// that we don't do weird blends between 180 degree apart rotations
	GetComponent.<Animation>().Play ("walljump");
	//SendMessage("SyncAnimation", "walljump");
}

@script AddComponentMenu ("Third Person Player/Third Person Player Animation")