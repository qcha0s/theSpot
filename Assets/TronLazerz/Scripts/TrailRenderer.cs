// var time : float = 2.0;
// var rate : int = 10;
 
// private var arv3 : Vector3[];
// private var head : int;
// private var tail = 0;
// private var sliceTime : float = 1.0 / rate;
// var hit : RaycastHit; 
 
// function Start () {
//     arv3 = new Vector3[(Mathf.RoundToInt(time * rate) + 1)];
     
//     for (var i = 0; i < arv3.Length; i++)
//         arv3[i] = transform.position;
//     head = arv3.Length-1;
//     StartCoroutine(CollectData());
// }
 
// function CollectData() : IEnumerator {
//     while (true) {
//         if (transform.position != arv3[head]) {
//             head = (head + 1) % arv3.Length;
//             tail = (tail + 1) % arv3.Length;
//             arv3[head] = transform.position;
//         }
//         yield WaitForSeconds(sliceTime);
//     }
// }
 
// function Update() {
//     if (Hit())
//         Debug.Log("I hit: "+hit.collider.name);
// }
 
// function Hit() : boolean {
//     var i = head;
//     var j = (head  - 1);
//     if (j < 0) j = arv3.Length - 1;
     
//     while (j != head) {
         
//         if (Physics.Linecast(arv3[i], arv3[j], hit))
//             return true;
//         i = i - 1; 
//         if (i < 0) i = arv3.Length - 1;
//         j = j - 1;
//         if (j < 0) j = arv3.Length - 1;
//     }
//     return false;
// }