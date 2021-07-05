import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit-emp',
  templateUrl: './add-edit-emp.component.html',
  styleUrls: ['./add-edit-emp.component.css']
})
export class AddEditEmpComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() emp:any;
  EmployeeId!: string;
  EmployeeName!:string;
  Department!:string;
  DateOfJoining!:string;
  PhotoFileName!:string;
  PhotoFilePath!:string;

  DepartmentList:any=[];

  ngOnInit(): void {
    this.loadDepartmentList();
   /** this.EmployeeId=this.emp.EmployeeId;
    this.EmployeeName=this.emp.EmployeeName;
    this.Department=this.emp.Department;
    this.DateOfJoining=this.emp.DateOfJoining;
    this.PhotoFileName=this.emp.PhotoFileName;
     */
  }
loadDepartmentList(){
  this.service.GetAllDepartmentNames().subscribe((data:any)=>{
    this.DepartmentList=data;

    this.EmployeeId=this.emp.EmployeeId;
    this.EmployeeName=this.emp.EmployeeName;
    this.Department=this.emp.Department;
    this.DateOfJoining=this.emp.DateOfJoining;
    this.PhotoFileName=this.emp.PhotoFileName;
    this.PhotoFilePath=this.service.PhotoUrl + this.emp.PhotoFileName;
  });
}

  addEmployee(){
   var val = {EmployeeId:this.emp.EmployeeId,
              EmployeeName:this.emp.EmployeeName,
            Department:this.emp.Department,
          DateOfJoining:this.emp.DateOfJoining,
        PhotoFileName:this.emp.PhotoFileName};

              this.service.addEmployee(val).subscribe(res=>{
                alert(res.toString());
  });
}
  updateEmployee(){
    var val = {EmployeeId:this.emp.EmployeeId,
      EmployeeName:this.emp.EmployeeName,
    Department:this.emp.Department,
  DateOfJoining:this.emp.DateOfJoining,
PhotoFileName:this.emp.PhotoFileName};

      this.service.updateEmployee(val).subscribe(res=>{
        alert(res.toString());
});
  }

  uploadPhoto(event:any){
    var file = event.target.files[0];
    const formData: FormData =new FormData();
    formData.append("uploadedFile",file.name);

    this.service.UploadPhoto(formData).subscribe((data:any)=>{
      this.PhotoFileName=data.toString();
      this.PhotoFilePath=this.service.PhotoUrl+ this.PhotoFileName;
    })
  }

}
